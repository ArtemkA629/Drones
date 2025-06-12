using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent), typeof(LineRenderer))]
public class Drone : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Storage _storage;
    [SerializeField] private DroneConfig _config;

    private ISourceFinder _sourceFinder;
    private IDroneMovement _movement;
    private ReachingChecker _reachingChecker;
    private PathShower _pathShower;
    private DroneState _state;
    private Goal _currentGoal;
    private Coroutine _goalReachingCoroutine;
    private Source _carryingSource;

    [Inject]
    public void Init(SourcesPool sourcesPool)
    {
        _sourceFinder = new NearestSourceFinder(transform, sourcesPool);
        _movement = new NavMeshMovement(_agent);
        _reachingChecker = new (_agent, _config.ReachDistance);
        _pathShower = new PathShower(_agent, _lineRenderer, _config.Material);
        _state = DroneState.SourceFinding;

        _reachingChecker.GoalReached += OnGoalReached;
    }

    private void Update()
    {
        _pathShower.Show();

        if (_state == DroneState.SourceFound)
            return;

        var nearestGoal = _sourceFinder.Find();
        if (nearestGoal == null || !nearestGoal.gameObject.activeInHierarchy)
            ResetGoal();
        else if (nearestGoal == _currentGoal)
            return;

        ChangeGoal(nearestGoal);
    }

    private void OnDisable()
    {
        if (_carryingSource != null)
            _carryingSource.OnPicked -= OnSourcePicked;

        StopReaching();
        _currentGoal = null;
        _carryingSource = null;
    }

    private void OnDestroy()
    {
        _reachingChecker.GoalReached -= OnGoalReached;
    }

    private void ChangeGoal(Goal goal)
    {
        _currentGoal = goal;
        StopReaching();
        ApplyMovement();
    }

    private void ResetGoal()
    {
        _state = DroneState.SourceFinding;
        _currentGoal = null;
        StopReaching();
    }

    private void StopReaching()
    {
        if (_goalReachingCoroutine != null)
            StopCoroutine(_goalReachingCoroutine);
    }

    private void OnGoalReached(Goal goal)
    {
        if (goal is Storage storage)
        {
            _state = DroneState.SourceFinding;
            storage.OnDroneReached();
            _currentGoal = null;
        }
        else
        {
            _state = DroneState.SourceFound;
            var source = (Source)goal;
            ApplyPicking(source);
            _currentGoal = _storage;
        }
    }

    private void ApplyMovement()
    {
        var movingPosition = _currentGoal == null ? Vector3.zero : _currentGoal.transform.position;
        _movement.MoveTo(movingPosition);
        _goalReachingCoroutine = StartCoroutine(_reachingChecker.CheckDistance(_currentGoal));
    }

    private void ApplyPicking(Source source)
    {
        source.OnPicked += OnSourcePicked;
        source.OnDroneReached(transform);
        _carryingSource = source;
    }

    private void OnSourcePicked(Source source)
    {
        ApplyMovement();
        source.OnPicked -= OnSourcePicked;
        _carryingSource = null;
    }
}

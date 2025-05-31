using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Drone : MonoBehaviour
{
    [Inject] private SourcesPool _sourcesPool;

    [SerializeField] private NavMeshAgent _agent;

    private ISourceFinder _sourceFinder;
    private IDroneMovement _movement;
    private IReachingChecker _reachingChecker;
    private DroneInfo _info;
    private GameObject _currentGoal;
    private Coroutine _goalReachingCoroutine;
    private Source _carryingSource;

    public void Init(DroneInfo info)
    {
        _sourceFinder = new NearestSourceFinder(transform, _sourcesPool);
        _movement = new NavMeshMovement(_agent);
        _reachingChecker = new ClassicReachingChecker(_agent);
        _info = info;

        _reachingChecker.GoalReached += OnGoalReached;
    }

    private void Update()
    {
        if (_currentGoal == _info.Storage)
            return;

        var nearestGoal = _sourceFinder.Find()?.gameObject;
        if (nearestGoal == _currentGoal || nearestGoal == null)
            return;

        _currentGoal = nearestGoal;
        if (_goalReachingCoroutine != null)
            StopCoroutine(_goalReachingCoroutine);
        ApplyMovement();
    }

    private void OnDisable()
    {
        _reachingChecker.GoalReached -= OnGoalReached;
        if (_carryingSource != null)
            _carryingSource.OnCollected -= OnSourceCollected;
    }

    private void OnGoalReached(GameObject reachedGoal)
    {
        if (reachedGoal == _info.Storage)
        {
            if (reachedGoal.TryGetComponent(out Storage storage))
                storage.OnDroneReached();
            else
                throw new Exception("storage not find");
            _currentGoal = null;
        }
        else
        {
            if (reachedGoal.TryGetComponent(out Source source))
                ApplyCollect(source);
            else
                throw new Exception("source not find");
            _currentGoal = _info.Storage;
        }
    }

    private void ApplyMovement()
    {
        _movement.MoveTo(_currentGoal.transform.position);
        _goalReachingCoroutine = StartCoroutine(_reachingChecker.CheckDistance(_currentGoal));
    }

    private void ApplyCollect(Source source)
    {
        source.OnCollected += OnSourceCollected;
        source.Collect(this);
        _carryingSource = source;
    }

    private void OnSourceCollected(Source source)
    {
        ApplyMovement();
        source.OnCollected -= OnSourceCollected;
        _carryingSource = null;
    }
}

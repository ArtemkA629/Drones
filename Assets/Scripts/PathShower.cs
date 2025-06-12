using UnityEngine;
using UnityEngine.AI;

public class PathShower
{
    public static bool IsShowingPath;

    private NavMeshAgent _agent;
    private LineRenderer _renderer;

    public PathShower(NavMeshAgent agent, LineRenderer renderer, Material material)
    {
        _agent = agent;
        _renderer = renderer;
        _renderer.material = material;
    }

    public void Show()
    {
        if (!IsShowingPath)
        {
            _renderer.positionCount = 0;
            return;
        }

        _renderer.positionCount = _agent.path.corners.Length;
        _renderer.SetPositions(_agent.path.corners);
    }
}

using UnityEngine;
using Zenject;

public class NearestSourceFinder : ISourceFinder
{
    private readonly SourcesPool _sourcesPool;
    private readonly Transform _finder;

    public NearestSourceFinder(Transform finder, SourcesPool sourcesPool)
    {
        _finder = finder;
        _sourcesPool = sourcesPool;
    }

    public Source Find()
    {
        float minDistance = float.MaxValue;
        Source nearestSource = null;

        foreach (var source in _sourcesPool.GetAllActive())
        {
            var distance = Vector3.Distance(_finder.position, source.transform.position);
            if (distance < minDistance && !source.IsTaking)
            {
                minDistance = distance;
                nearestSource = source;
            }
        }

        return nearestSource;
    }
}

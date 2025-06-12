using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SourcesPool : MonoBehaviour
{
    [SerializeField] private SourcesPoolConfig _config;
    [SerializeField] private Transform _poolContainer;

    private IObjectPool<Source> _pool;

    private void Awake()
    {
        InitializePool();
    }

    public Source GetSource()
    {
        return _pool.Get();
    }

    public void ReleaseSource(Source source)
    {
        _pool.Release(source);
    }

    public Source[] GetAllActive()
    {
        var sources = new List<Source>();
        foreach (var source in _poolContainer.GetComponentsInChildren<Source>())
        {
            if (source.gameObject.activeInHierarchy)
                sources.Add(source);
        }

        return sources.ToArray();
    }

    private void InitializePool()
    {
        if (_poolContainer == null)
        {
            _poolContainer = new GameObject("CoinPoolContainer").transform;
            _poolContainer.SetParent(transform);
        }

        _pool = new ObjectPool<Source>(
            CreatePooledItem,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            true,
            _config.DefaultCapacity,
            _config.MaxPoolSize);
    }

    private Source CreatePooledItem()
    {
        Source source = Instantiate(_config.SourcePrefab, _poolContainer);
        source.Init();
        return source;
    }

    private void OnTakeFromPool(Source source)
    {
        source.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Source source)
    {
        source.gameObject.SetActive(false);
        source.transform.SetParent(_poolContainer);
    }

    private void OnDestroyPoolObject(Source source)
    {
        Destroy(source.gameObject);
    }
}

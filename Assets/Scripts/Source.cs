using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Source : MonoBehaviour, IGoal, ICollectable
{
    protected IObjectPool<Source> _pool;
    protected bool _isTaking;

    private Vector3 _initialScale;

    public abstract event Action<Source> OnCollected;

    protected Vector3 InitialScale => _initialScale;

    public bool IsTaking => _isTaking;

    protected virtual void Start()
    {
        _initialScale = transform.localScale;
    }

    public void SetPoolReference(IObjectPool<Source> pool)
    {
        _pool = pool;
    }

    protected void ReturnToPool()
    {
        if (_pool != null)
            _pool.Release(this);
        else
            Destroy(gameObject);
    }

    public abstract void Collect(Drone drone);

    public virtual void ResetParams()
    {
        if (_initialScale == Vector3.zero)
            return;

        transform.localScale = _initialScale;
        _isTaking = false;
    }
}

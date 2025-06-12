using DG.Tweening;
using System;
using UnityEngine;

public class Source : Goal
{
    [SerializeField] private SourceConfig _config;

    private Vector3 _initialScale;
    private bool _isTaking;

    public event Action<Source> OnPicked;

    public Vector3 InitialScale => _initialScale;
    public bool IsTaking => _isTaking;

    public void Init()
    {
        _initialScale = transform.localScale;
        StartInfiniteRotation();
    }

    public void OnDroneReached(Transform drone)
    {
        _isTaking = true;
        AnimatePicking(drone);
    }

    private void AnimatePicking(Transform drone)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(drone.position, _config.DestroyDuration).SetEase(Ease.Linear));
        sequence.Join(transform.DOScale(InitialScale * _config.ScaleDownFactor, _config.DestroyDuration));
        sequence.OnComplete(() =>
        {
            OnPicked?.Invoke(this);
            Destroy(gameObject);
        });
    }

    private void StartInfiniteRotation()
    {
        transform.DORotate(new Vector3(90f, 360f, 0f), _config.RotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}

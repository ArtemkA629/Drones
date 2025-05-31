using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Coin : Source
{
    private static float _rotationDuration = 3f;
    private static float _destroyDuration = 0.5f;
    private static float _scaleDownFactor = 0.3f;

    public override event Action<Source> OnCollected;

    protected override void Start()
    {
        base.Start();
        StartInfiniteRotation();
    }

    public override void Collect(Drone drone)
    {
        _isTaking = true;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(drone.transform.position, _destroyDuration)
            .SetEase(Ease.Linear));
        sequence.Join(transform.DOScale(InitialScale * _scaleDownFactor, _destroyDuration));

        sequence.OnComplete(() =>
        {
            OnCollected?.Invoke(this);
            ReturnToPool();
        });
    }

    private void StartInfiniteRotation()
    {
        transform.DORotate(new Vector3(90f, 360f, 0f), _rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}

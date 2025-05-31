using UnityEngine;

public class Chest : Storage
{
    private static string _openTrigger = "Open";

    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _openParticle;

    public override void OnDroneReached()
    {
        _animator.SetTrigger(_openTrigger);
    }

    public void OnEndAnimation()
    {
        _openParticle.Play();
    }
}

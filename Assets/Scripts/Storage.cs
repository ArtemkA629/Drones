using UnityEngine;

public class Storage : Goal
{
    private static string _openTrigger = "Open";

    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _openParticle;
    [SerializeField] private StorageInventoryView _inventoryView;

    private StorageInventory _inventory;

    private void Start()
    {
        _inventory = new();
        _inventoryView.Init(_inventory);
    }

    public void OnDroneReached()
    {
        _inventory.AddCoin();
        _animator.SetTrigger(_openTrigger);
    }

    public void OnEndAnimation()
    {
        _openParticle.Play();
    }
}

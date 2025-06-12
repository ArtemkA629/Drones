using TMPro;
using UnityEngine;

public class StorageInventoryView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _inventoryText;

    private StorageInventory _inventory;

    public void Init(StorageInventory inventory)
    {
        _inventory = inventory;
        _inventory.Changed += OnInventoryAmountChanged;
    }

    private void OnDestroy()
    {
        _inventory.Changed -= OnInventoryAmountChanged;
    }

    private void OnInventoryAmountChanged()
    {
        _inventoryText.text = _inventory.CoinsAmount.ToString();
    }
}

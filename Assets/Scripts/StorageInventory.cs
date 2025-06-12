using System;

public class StorageInventory
{
    private int _coinsAmount;

    public event Action Changed;

    public int CoinsAmount => _coinsAmount;

    public void AddCoin()
    {
        _coinsAmount++;
        Changed?.Invoke();
    }
}

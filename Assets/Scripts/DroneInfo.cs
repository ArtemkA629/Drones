using System;
using UnityEngine;

[Serializable]
public struct DroneInfo
{
    [SerializeField] private GameObject _storage;

    public GameObject Storage => _storage;
}

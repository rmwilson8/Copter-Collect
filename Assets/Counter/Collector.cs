using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public event EventHandler OnTrashCollected;
    public event EventHandler OnLevelCompleted;
    public int Count { get; private set; } = 0;
    [field: SerializeField]public int RequiredCount { get; private set; }

    [SerializeField] private GameObject _trashPrefab;

    private void Start()
    {
        Count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IPickup>(out IPickup pickup))
        {
            pickup.ReturnToPool();
            Count += 1;
            OnTrashCollected?.Invoke(this, EventArgs.Empty);
            InstantiateTrash(other.transform.position);

            if(Count >= RequiredCount) 
            {
                Debug.Log("Level Completed!");
                OnLevelCompleted?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void InstantiateTrash(Vector3 position)
    {
        GameObject trashObject = Instantiate(_trashPrefab, position, _trashPrefab.transform.rotation, transform);
    }
}

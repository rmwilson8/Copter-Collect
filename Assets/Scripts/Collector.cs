﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public static event EventHandler OnAnyTrashCollected;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private GameObject _trashPrefab;


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IPickup>(out IPickup pickup))
        {
            pickup.ReturnToPool();
            
            OnAnyTrashCollected?.Invoke(this, EventArgs.Empty);
            InstantiateTrash(other.transform.position);
        }
    }

    private void InstantiateTrash(Vector3 position)
    {
        GameObject trashObject = Instantiate(_trashPrefab, position + Vector3.up * .01f, _trashPrefab.transform.rotation, transform);
    }
}

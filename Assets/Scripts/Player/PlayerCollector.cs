using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollector : MonoBehaviour
{
    public event EventHandler<float> OnFuelPickedUp;
    public bool IsCarrying { get; private set; }

    [Tooltip("Pickup Variables")]
    [SerializeField] private Transform _pickupSlot;
    private Transform PickedUpTransform;

    private InputReader _inputReader;

    private void OnEnable()
    {
        _inputReader = GetComponent<InputReader>();
        _inputReader.OnPickupEvent += HandlePickup;
        _inputReader.OnDropEvent += HandleDrop;
    }

    private void OnDisable()
    {
        _inputReader.OnPickupEvent -= HandlePickup;
        _inputReader.OnDropEvent -= HandleDrop;
    }

    void Update()
    {
        if (IsCarrying)
        {
            PickedUpTransform.position = _pickupSlot.position;
        }
    }

    private void HandlePickup(object sender, EventArgs e)
    {
        if(!IsCarrying) // do not want to pickup if already carrying
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.TryGetComponent<IPickup>(out IPickup pickup))
                {
                    if(pickup is Trash)
                    {
                        //Debug.Log("Picked Up Trash");
                        PickedUpTransform = hit.transform;
                        IsCarrying = true;
                        pickup.Pickup(_pickupSlot);
                    }

                    else if (pickup is Fuel)
                    {
                        Debug.Log("Picked Up Fuel");
                        pickup.ReturnToPool();
                        OnFuelPickedUp?.Invoke(this, 10f); // REPLACE MAGIC NUMBER!!!!!!
                    }
                }
            }
        }
    }
    private void HandleDrop(object sender, EventArgs e)
    {
        if (PickedUpTransform != null)
        {
            //Clears the picked up objects parent and clears _pickedUpTransform field
            IsCarrying = false;
            PickedUpTransform.GetComponent<IPickup>().Release();
            PickedUpTransform = null;
        }
    }
}

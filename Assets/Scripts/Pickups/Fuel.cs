using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour, IPickup
{
    public bool PickedUp { get; private set; } // do not want to move when picked up by helicopter
    [SerializeField] private float _moveSpeed = 2.5f;

    void Update()
    {
        if (!PickedUp)
        {
            transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
        }

        else
        {
            transform.position = transform.parent.position;
        }
    }

    public void Pickup(Transform parent)
    {
        transform.parent = parent;
        PickedUp = true;
    }
    public void Release()
    {
        transform.parent = null;
        PickedUp = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Barrier"))
        {
            gameObject.SetActive(false);
        }
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
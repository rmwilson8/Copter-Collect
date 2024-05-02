using UnityEngine;

public interface IPickup
{
    void Pickup(Transform parent);
    void Release();

    void ReturnToPool();
}

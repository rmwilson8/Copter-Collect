using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject _objectToPool;
    [SerializeField] int _poolCount;

    private List<GameObject> _objectPool = new List<GameObject>();

    private void Awake()
    {
        CreateObjectPool();    
    }

    private void CreateObjectPool()
    {
        for(int i = 0; i < _poolCount; i++) 
        {
            GameObject poolObject = Instantiate(_objectToPool, transform.position, Quaternion.identity, transform);
            poolObject.SetActive(false);
            _objectPool.Add(poolObject);
        }
    }

    public GameObject GetObjectFromPool()
    {
        foreach(GameObject poolObject in _objectPool)
        {
            if (!poolObject.activeInHierarchy)
            {
                poolObject.SetActive(true);
                poolObject.transform.rotation = _objectToPool.transform.rotation; // added so fuel cannister are rotated properly
                return poolObject;
            }
        }

        Debug.Log("No additional objects currently availible in pool.");
        return null;
    }
}

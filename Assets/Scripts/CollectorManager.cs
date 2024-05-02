using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorManager : MonoBehaviour
{
    public event EventHandler OnLevelCompleted;
    [field: SerializeField] public int RequiredCount { get; private set; }
    public int Count { get; private set; } = 0;

    [SerializeField] ObjectPool _collectorObjectPool;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _spawnTime;



    private void OnEnable()
    {
        Collector.OnAnyTrashCollected += Collector_OnAnyTrashCollected;
    }

    private void Collector_OnAnyTrashCollected(object sender, EventArgs e)
    {
        Count += 1;


        if (Count >= RequiredCount)
        {
            Debug.Log("Level Completed!");
            OnLevelCompleted?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDisable()
    {
        Collector.OnAnyTrashCollected -= Collector_OnAnyTrashCollected;
    }

    private void Start()
    {
        Count = 0;
        StartCoroutine(SpawnCollectorRoutine());
    }

    private IEnumerator SpawnCollectorRoutine()
    {
        for(int i = 0; i <_collectorObjectPool.PoolCount; i++) 
        {
            GameObject collector = _collectorObjectPool.GetObjectFromPool();

            if(collector != null)
            {
                collector.transform.position = _spawnPosition.position;
            }

            yield return new WaitForSeconds(_spawnTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _trashMinSpawnInterval = 0.5f;
    [SerializeField] private float _trashMaxSpawnInterval = 3f;
    [SerializeField] private float _fuelMinSpawnInterval = 0.5f;
    [SerializeField] private float _fuelMaxSpawnInterval = 10f;
    [SerializeField] private ObjectPool _trashObjectPool;
    [SerializeField] private ObjectPool _fuelObjectPool;
    [SerializeField] private Transform[] _spawnPositions;

    bool _isPlaying;

    void Start()
    {
        StartCoroutine(SpawnTrashObjectsRoutine());
        StartCoroutine(SpawnFuelObjectsRoutine());
    }

    private IEnumerator SpawnTrashObjectsRoutine()
    {
        while (GameManager.Instance.IsPlaying)
        {
            yield return new WaitForSeconds(Random.Range(_trashMinSpawnInterval, _trashMaxSpawnInterval));
            GameObject spawnedObject = _trashObjectPool.GetObjectFromPool();

            if (spawnedObject != null) 
            {
                int randomIndex = Random.Range(0, _spawnPositions.Length);
                Vector3 spawnPosition = _spawnPositions[randomIndex].position;
                spawnedObject.transform.position = spawnPosition;
            }
        }
    }

    private IEnumerator SpawnFuelObjectsRoutine()
    {
        while(GameManager.Instance.IsPlaying) 
        {
            yield return new WaitForSeconds(Random.Range(_fuelMinSpawnInterval, _fuelMaxSpawnInterval));
            GameObject spawnedObject = _fuelObjectPool.GetObjectFromPool();

            if ( spawnedObject != null) 
            {
                int randomIndex = Random.Range(0, _spawnPositions.Length);
                Vector3 spawnPosition = _spawnPositions[randomIndex].position;
                spawnedObject.transform.position = spawnPosition;
            }
        }
    }
}

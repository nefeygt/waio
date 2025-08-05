using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    public GameObject cherryPrefab;
    public int cherryPoolSize;
    public List<Vector3> cherrySpawnPoints;
 

    void Start()
    {
        if (ObjectPoolManager.Instance != null)
        {
            ObjectPoolManager.Instance.CreatePool(cherryPrefab, cherryPoolSize);
        }
        SpawnObjects(cherryPrefab, cherrySpawnPoints);
    }

    private void SpawnObjects(GameObject prefab, List<Vector3> spawnPoints)
    {
        if (ObjectPoolManager.Instance == null)
        {
            Debug.LogError("ObjectPoolManager not found! Make sure it's in the scene.");
            return;
        }

        foreach (Vector3 spawnPoint in spawnPoints)
        {
            ObjectPoolManager.Instance.GetObjectFromPool(prefab, spawnPoint, Quaternion.identity);
        }
    }
}
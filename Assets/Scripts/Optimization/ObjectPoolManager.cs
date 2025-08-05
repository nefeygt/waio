using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary;

    public GameObject poolParent;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
    }

    public void CreatePool(GameObject prefab, int poolSize)
    {
        if (poolDictionary.ContainsKey(prefab))
        {
            return;
        }

        Queue<GameObject> objectPool = new Queue<GameObject>();

        GameObject parentObject = new GameObject(prefab.name + " Pool");
        parentObject.transform.SetParent(poolParent.transform);

        parentObject.transform.localPosition = Vector3.zero;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, parentObject.transform);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        poolDictionary.Add(prefab, objectPool);
    }

    public GameObject GetObjectFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool for " + prefab.name + " not found!");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[prefab].Dequeue();

        objectToSpawn.SetActive(true);

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[prefab].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
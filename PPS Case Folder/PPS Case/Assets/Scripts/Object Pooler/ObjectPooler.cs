using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }



    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);


        }


    }

    public GameObject SpawnFromPool(string _tag, Vector3 _position, Quaternion _rotation)
    {

        if (!poolDictionary.ContainsKey(_tag))
        {
            return null;
        }

        GameObject objectToSpawn = poolDictionary[_tag].Dequeue();


        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = _position;
        objectToSpawn.transform.rotation = _rotation;


        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[_tag].Enqueue(objectToSpawn);

        return objectToSpawn;

    }



}

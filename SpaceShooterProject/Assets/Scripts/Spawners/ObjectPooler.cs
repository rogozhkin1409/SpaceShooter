using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public enum ObjectType { Bullet, Asteroid, Explosion }

    [System.Serializable]
    public class Pool
    {
        public ObjectType objectType;
        public PoolObject prefab;
        public int poolSize;
    }

    [SerializeField] private List<Pool> _pools;
    private Dictionary<ObjectType, Queue<PoolObject>> _poolDictionary;

    public static ObjectPooler Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        LevelController.Instance.OnPlayingStateChanged += HandlePlayingStateChanged;

        _poolDictionary = new Dictionary<ObjectType, Queue<PoolObject>>();
        foreach (Pool pool in _pools)
        {
            Queue<PoolObject> objectPool = new Queue<PoolObject>();
            for (int i = 0; i < pool.poolSize; i++)
            {
                PoolObject obj = Instantiate(pool.prefab);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.objectType, objectPool);
        }
    }

    private void OnDestroy()
    {
        if (LevelController.Instance)
            LevelController.Instance.OnPlayingStateChanged -= HandlePlayingStateChanged;
    }

    private void HandlePlayingStateChanged(bool state)
    {
        if (!state)
        {
            foreach(var pool in _poolDictionary)
            {
                foreach (var obj in pool.Value)
                {
                    obj.gameObject.SetActive(false);
                }
            }    
        }
    }

    public PoolObject Spawn(ObjectType objectType, Vector3 position, Quaternion rotation)
    {
        PoolObject objectToSpawn = _poolDictionary[objectType].Dequeue();

        objectToSpawn.gameObject.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectToSpawn.OnSpawn();

        _poolDictionary[objectType].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}

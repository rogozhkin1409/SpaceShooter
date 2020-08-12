using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private int _minSpawnAmount;
    [SerializeField] private int _maxSpawnAmount;
    [SerializeField] private float _spawnCooldown;

    [SerializeField] private float _spawnWidth;

    private ObjectPooler _objectPooler;
    private float _nextSpawnTime;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        if (LevelController.Instance.IsPlaying)
        {
            if (Time.time > _nextSpawnTime)
            {
                _nextSpawnTime = Time.time + _spawnCooldown;
                SpawnWave();
            }
        }
    }

    private void SpawnWave()
    {
        int amount = Random.Range(_minSpawnAmount, _maxSpawnAmount + 1);
        for (int i = 0; i < amount; i++)
        {
            Vector2 position = new Vector2(Random.Range(transform.position.x - _spawnWidth, transform.position.x + _spawnWidth), transform.position.y);
            _objectPooler.Spawn(ObjectPooler.ObjectType.Asteroid, position, Quaternion.identity);
        }
    }
}

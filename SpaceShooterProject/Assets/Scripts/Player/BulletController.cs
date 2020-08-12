using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : PoolObject
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 2f;

    private float _spawnTime;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void OnSpawn()
    {
        _spawnTime = Time.time;
        _rigidbody.velocity = Vector2.up * _speed;
    }

    private void Update()
    {
        if (Time.time > _spawnTime + _lifeTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IHitable>(out var hitable))
        {
            hitable.Hit();
            gameObject.SetActive(false);
        }
    }
}

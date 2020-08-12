using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class AsteroidController : PoolObject, IHitable
{
    [SerializeField] private Sprite[] _asteroidSprites;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;

    private static ObjectPooler _objectPooler;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Sprite sprite = _asteroidSprites[Random.Range(0, _asteroidSprites.Length)];
        GetComponent<SpriteRenderer>().sprite = sprite;
        GetComponent<CircleCollider2D>().radius = Mathf.Max(sprite.bounds.size.x, sprite.bounds.size.y) * .64f;
        if (!_objectPooler)
            _objectPooler = ObjectPooler.Instance;
    }

    public override void OnSpawn()
    {
        float speed = Random.Range(_minSpeed, _maxSpeed);
        _rigidbody.velocity = Vector2.down * speed;
        _rigidbody.angularVelocity = (Random.Range(0, 1f) > .5f ? speed : -speed) * 10f;
    }

    public void Hit()
    {
        _objectPooler.Spawn(ObjectPooler.ObjectType.Explosion, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}

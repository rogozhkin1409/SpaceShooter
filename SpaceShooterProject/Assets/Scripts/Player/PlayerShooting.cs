using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerShooting : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)] private float _fireRate;
    [SerializeField] private Transform[] _fireSpawnPoints;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _fireClips;

    private float _nextFireTime;
    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _audioSource.volume = AudioManager.Instance.EffectsVolume;
    }

    public void Shoot()
    {
        if (Time.time < _nextFireTime)
            return;

        _nextFireTime = Time.time + 1f / _fireRate;
        PlayRandomSound();
        for(int i = 0; i < _fireSpawnPoints.Length; i++)
            _objectPooler.Spawn(ObjectPooler.ObjectType.Bullet, _fireSpawnPoints[i].position, _fireSpawnPoints[i].rotation);
    }

    private void PlayRandomSound()
    {
        _audioSource.clip = _fireClips[Random.Range(0, _fireClips.Length)];
        _audioSource.Play();
    }
}

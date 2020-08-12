using UnityEngine;

public class Explosion : PoolObject
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private AudioSource _explosionSound;

    private void Awake()
    {
        _explosionSound.volume = AudioManager.Instance.EffectsVolume;
    }

    public override void OnSpawn()
    {
        _explosionEffect.Play();
        _explosionSound.pitch = Random.Range(.8f, 1.2f);
        _explosionSound.Play();
    }
}

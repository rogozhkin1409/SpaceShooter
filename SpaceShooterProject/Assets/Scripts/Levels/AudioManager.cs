using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource _musicAudioSource;

    private float _musicVolume;
    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = Mathf.Clamp01(value);
            _musicAudioSource.volume = _musicVolume;
            PlayerPrefs.SetFloat("MusicVolume", _musicVolume);
        }
    }

    private float _effectsVolume;
    public float EffectsVolume
    {
        get => _effectsVolume;
        set
        {
            _effectsVolume = Mathf.Clamp01(value);
            PlayerPrefs.SetFloat("EffectsVolume", _effectsVolume);
        }
    }

    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        _musicAudioSource = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("MusicVolume"))
            MusicVolume = 1f;
        else
            _musicVolume = PlayerPrefs.GetFloat("MusicVolume");

        if (!PlayerPrefs.HasKey("EffectsVolume"))
            EffectsVolume = 1f;
        else
            _effectsVolume = PlayerPrefs.GetFloat("EffectsVolume");

        DontDestroyOnLoad(gameObject);
    }
}

using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public event System.Action<bool> OnPlayingStateChanged = delegate { };

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _timeLeftText;
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _loseUI;
    [SerializeField] private GameObject _winUI;

    [Header("Player")]
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector2 _playerStartPosition;

    [Header("Sounds")]
    [SerializeField] private AudioSource _gameOverAudioSource;
    [SerializeField] private AudioClip _winAudioClip;
    [SerializeField] private AudioClip _loseAudioClip;

    private float _timeLeft;
    private bool _isPlaying;
    public bool IsPlaying
    {
        get => _isPlaying;
        private set
        {
            OnPlayingStateChanged(value);
            _isPlaying = value;
        }
    }

    private static LevelController _instance;
    public static LevelController Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<LevelController>();

            return _instance;
        }
        private set => _instance = value;
    }

    private void Awake()
    {
        Instance = this;
        _gameOverAudioSource.volume = AudioManager.Instance.EffectsVolume;
        StartLevel();
    }

    private void Update()
    {
        if (!IsPlaying)
            return;

        _timeLeft -= Time.deltaTime;
        _timeLeftText.text = _timeLeft.ToString("F1");
        if (_timeLeft < 0)
        {
            GameOver(true);
        }
    }

    public void GameOver(bool isWin)
    {
        IsPlaying = false;
        _gameUI.SetActive(false);
        _player.SetActive(false);
        if (isWin)
        {
            LevelManager.Instance.CompleteCurrentLevel();
            _gameOverAudioSource.clip = _winAudioClip;
            _winUI.SetActive(true);
        }
        else
        {
            _gameOverAudioSource.clip = _loseAudioClip;
            _loseUI.SetActive(true);
        }

        _gameOverAudioSource.Play();
    }

    public void StartLevel()
    {
#if UNITY_EDITOR
        if (LevelManager.Instance)
            _timeLeft = LevelManager.Instance.CurrentLevel.SecondsToWin;
        else
            _timeLeft = 60f;
#else
        _timeLeft = LevelManager.Instance.CurrentLevel.SecondsToWin;
#endif

        IsPlaying = true;
        _loseUI.SetActive(false);
        _winUI.SetActive(false);
        _gameUI.SetActive(true);
        _player.SetActive(true);
        _player.transform.position = _playerStartPosition;
        _levelNumberText.text = $"LEVEL {LevelManager.Instance.CurrentLevel.LevelNumber}";
    }

    public void NextLevel()
    {
        LevelManager.Instance.SetNextLevel();
        StartLevel();
    }

    public void OpenMenu()
    {
        LevelManager.Instance.OpenMenu();
    }
}

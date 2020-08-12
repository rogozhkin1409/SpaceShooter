using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _effectsVolume;

    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
        _musicVolume.value = _audioManager.MusicVolume;
        _effectsVolume.value = _audioManager.EffectsVolume;
    }

    public void OpenMenu()
    {
        _settingsCanvas.SetActive(false);
        _menuCanvas.SetActive(true);
    }

    public void OpenSettings()
    {
        _menuCanvas.SetActive(false);
        _settingsCanvas.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene("LevelsScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MusicSliderChanged()
    {
        _audioManager.MusicVolume = _musicVolume.value;
    }

    public void EffectsSliderChanged()
    {
        _audioManager.EffectsVolume = _effectsVolume.value;
    }
}

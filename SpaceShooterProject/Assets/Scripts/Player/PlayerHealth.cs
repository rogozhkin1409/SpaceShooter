using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _lifes;
    [SerializeField] private TextMeshProUGUI _lifesText;

    private int _currentLifes;

    private void Awake()
    {
        LevelController.Instance.OnPlayingStateChanged += HanldePlayingStateChanged;
    }

    private void OnDestroy()
    {
        if (LevelController.Instance)
            LevelController.Instance.OnPlayingStateChanged -= HanldePlayingStateChanged;
    }

    private void HanldePlayingStateChanged(bool state)
    {
        if (state)
        {
            _currentLifes = _lifes;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        _lifesText.text = _currentLifes.ToString();
    }

    private void Hit()
    {
        _currentLifes--;
        UpdateUI();
        if (_currentLifes == 0)
            LevelController.Instance.GameOver(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AsteroidController>(out var asteroid))
        {
            asteroid.Hit();
            Hit();
        }
    }
}

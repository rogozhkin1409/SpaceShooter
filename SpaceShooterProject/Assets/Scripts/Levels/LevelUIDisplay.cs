using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelUIDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private Image _levelStateSprite;

    private Level _level;

    public void Initialize(Level level)
    {
        _levelNumberText.text = $"LEVEL {level.LevelNumber}";
        Sprite sprite = LevelUIManager.Instance.GetLevelStateSprite(level.State);
        if (sprite != null)
        {
            _levelStateSprite.sprite = sprite;
            _levelStateSprite.color = Color.white;
        }
        else
            _levelStateSprite.color = Color.clear;

        GetComponent<Button>().onClick.AddListener(() => LevelUIManager.Instance.OpenLevel(level));
    }

    public void OnLevelButtonClicked()
    {
        LevelUIManager.Instance.OpenLevel(_level);
    }
}

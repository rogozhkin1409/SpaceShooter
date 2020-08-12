using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private Transform _gridLayout;
    [SerializeField] private LevelUIDisplay _levelUI;
    [SerializeField] private Sprite _closedSprite;
    [SerializeField] private Sprite _completedSprite;

    public static LevelUIManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Level[] levels = LevelManager.Instance.Levels;
        foreach (var level in levels)
        {
            var levelUI = Instantiate(_levelUI, _gridLayout);
            levelUI.Initialize(level);
        }
    }

    public void OpenLevel(Level level)
    {
        if (level.State == Level.LevelState.Closed)
            return;

        LevelManager.Instance.OpenLevel(level);
    }

    public Sprite GetLevelStateSprite(Level.LevelState state)
    {
        if (state == Level.LevelState.Closed)
            return _closedSprite;
        if (state == Level.LevelState.Completed)
            return _completedSprite;

        return null;
    }
}

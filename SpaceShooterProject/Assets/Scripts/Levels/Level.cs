using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data")]
public class Level : ScriptableObject
{
    public enum LevelState { Opened, Closed, Completed }
    [SerializeField] private int _levelNumber;
    [SerializeField] private float _secondsToWin;
    [SerializeField] private LevelState _levelState;

    public int LevelNumber { get => _levelNumber; }
    public float SecondsToWin { get => _secondsToWin; }
    public LevelState State { get => _levelState; set => _levelState = value; }
}

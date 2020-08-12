using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] _levels;

    public Level[] Levels { get => _levels; }
    public Level CurrentLevel { get; private set; }

    public static LevelManager Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadLevelData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenMenu();
    }

    public void OpenLevel(Level level)
    { 
        CurrentLevel = level;
        SceneManager.LoadScene("GameScene");
    }

    public void SetNextLevel()
    {
        if (CurrentLevel.LevelNumber < _levels.Length)
        {
            CurrentLevel = _levels[CurrentLevel.LevelNumber];
        }
    }

    public void CompleteCurrentLevel()
    {
        CurrentLevel.State = Level.LevelState.Completed;
        if (CurrentLevel.LevelNumber < _levels.Length)
        {
            _levels[CurrentLevel.LevelNumber].State = Level.LevelState.Opened;
            
        }
        SaveLevelData();
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void SaveLevelData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/leveldata.rog";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        Level.LevelState[] levelStates = new Level.LevelState[_levels.Length];
        for (int i = 0; i < _levels.Length; i++)
            levelStates[i] = _levels[i].State;

        binaryFormatter.Serialize(fileStream, levelStates);

        fileStream.Close();
    }

    public void LoadLevelData()
    {
        string path = Application.persistentDataPath + "/leveldata.rog";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            Level.LevelState[] levelStates = binaryFormatter.Deserialize(fileStream) as Level.LevelState[];
            for (int i = 0; i < _levels.Length; i++)
                _levels[i].State = levelStates[i];

            fileStream.Close();
        }
    }
}

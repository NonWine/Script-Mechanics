using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private List<GameObject> _levels;
    [SerializeField] private int testLevelIndex;
    public int CurrentLevel { private set; get; }
    public int VisualCurrentLevel { private set; get; }

    private GameObject level;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        if (CurrentLevel == 0)
            VisualCurrentLevel = 0;
        VisualCurrentLevel = PlayerPrefs.GetInt("VisualCurrentLevel",1);

    }

    private void Start()
    {
    
        if (CurrentLevel >= _levels.Count)
            CurrentLevel = 0;
        level = Instantiate(_levels[CurrentLevel]);
    
    }

    public void FinishLevel()
    {
        CurrentLevel++;
        VisualCurrentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.SetInt("VisualCurrentLevel", VisualCurrentLevel);
    }

    public void LoadLevel()
    {
        
        Destroy(level);
        if (CurrentLevel >= _levels.Count)
        {
            CurrentLevel = 0;
            VisualCurrentLevel = 1;
        }
        level = Instantiate(_levels[CurrentLevel]);

    }

    [ContextMenu("SetTestLevel")]
    public void SetTestLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", testLevelIndex);
    }
}
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject _confettiFx;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _weaponPanel;
    [SerializeField] private GameObject _TutorialPanel;
    [SerializeField] private TextMeshProUGUI _levelText;
    private bool isFinish;
    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        _gamePanel.SetActive(true);
        _levelText.SetText("LEVEL " + (LevelManager.Instance.VisualCurrentLevel).ToString());
    }

    public void GameLose()
    {
        if (isFinish)
            return;
        isFinish = true;
        _losePanel.SetActive(true);
        _gamePanel.SetActive(false);
    }

    public void GameWin()
    {
       
        if (isFinish)
            return;
        isFinish = true;
        LevelManager.Instance.FinishLevel();
        if(_confettiFx != null)
        _confettiFx.SetActive(true);
        _gamePanel.SetActive(false);
        _winPanel.SetActive(true);
    }

    public void NextLevel()
    {
        isFinish = false;
        _winPanel.SetActive(false);
        _gamePanel.SetActive(true);
        LevelManager.Instance.LoadLevel();
        _levelText.SetText("LEVEL " + (LevelManager.Instance.VisualCurrentLevel).ToString());

    }

    public void RestartLevel()
    {
        isFinish = false;
        _gamePanel.SetActive(true);
        _losePanel.SetActive(false);
     
        LevelManager.Instance.LoadLevel();
    }
    public bool GetFinish()
    {
        return isFinish;
    }
    private void StartTutorial()
    {
        _TutorialPanel.SetActive(true);
    }
}
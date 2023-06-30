using UnityEngine;
using TMPro;
using UnityEngine.UI;
public abstract class BaseStatts: MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _maxLevel;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _textValue;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private int[] cost;
    [SerializeField] private int[] value;
    private int index;
    private int levelIndex;
    private bool isMax;
    private void Awake()
    {
        index = 0;
        levelIndex = 1;
    }

    private void SetMaxLevel()
    {
        _button.interactable = false;
        _maxLevel.SetActive(true);
    }
    
    public void LaunchUpgrade()
    {
        if(index == 1000)
        {
            SetMaxLevel();
            return;
        }
        SetCost();
        SetValue();
        SetLevel();
        _maxLevel.SetActive(false);
    }

    public abstract void Upgrade(WeaponStats stats);

    public int GetcostByIndex(int i) { return cost[i]; }

    public int GetCurrentCost() { return cost[index]; }

    public int GetCurrentValue() { return value[index]; }

    public void IncreaseIndex()
    {
        index++;
        levelIndex++;
        if(index == cost.Length)
        {
            isMax = true;
            SetMaxLevel();
        }
    }

    public void SetCost() => _costText.text = cost[index].ToString();

    public void SetValue() => _textValue.text = value[index].ToString();

    public void SetLevel() => _levelText.text = "Level " + levelIndex.ToString();
    
    public void SetIndex(int i) { index = i; levelIndex = i + 1; }

    public int GetCurrentIndex() { return index; }

    public bool isMaxLevel() {return  isMax; }

    public void SaveStat(string key, int value)
    {
        if (index == cost.Length)
            PlayerPrefs.SetInt(key, 1000);
        else
            PlayerPrefs.SetInt(key, value);
    }
}

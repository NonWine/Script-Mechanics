using UnityEngine;
public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private WeaponStats stats;
    [field:SerializeField]
    private BaseStatts[] _upgrades;
    private int[] _upgradeIndex = new int[3];

    private void Awake()
    {
        _upgradeIndex[0] = PlayerPrefs.GetInt("DamageIndex", 0);
        _upgradeIndex[1] = PlayerPrefs.GetInt("SpeedIndex", 0);
        _upgradeIndex[2] = PlayerPrefs.GetInt("HealthIndex", 0);
    }
    private void Start()
    {
        for(int i=0; i < _upgrades.Length;i++)
        {
            _upgrades[i].SetIndex(_upgradeIndex[i]);
            _upgrades[i].LaunchUpgrade();
        }
    }

    public void DamageUp() =>_upgrades[0].Upgrade(stats);

    public void SpeedUp() => _upgrades[1].Upgrade(stats);

    public void HealthUp() => _upgrades[2].Upgrade(stats);
   
}

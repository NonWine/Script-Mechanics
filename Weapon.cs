using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Other\n")]
    [SerializeField] private MobStats stats;
    [SerializeField] private Bullet[] bullets;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Bullet[] spamWeapons;
    private int indexWeapon;
    private void Awake()
    {
        indexWeapon = PlayerPrefs.GetInt("indexWeapon", indexWeapon);

    }

    private void Start()
    {
        weapons[indexWeapon].SetActive(true);
    }

    public void Shoot(Unit enemy)
    {
        foreach (var item in bullets)
        {
            if (item.isReload())
            {
                item.Draw(enemy);
            }
        }
    }

    public void SpamAbility()
    {
        foreach (var item in spamWeapons)
        {
            if (item.isReload())
            {
                item.Spam();
            }
        }
    }

    public void SetRifle()
    {
        weapons[2].SetActive(true);
        weapons[0].SetActive(false);
        weapons[1].SetActive(false);
        bullets[0].LockBullet();
        bullets[1].LockBullet();
        bullets[2].UnlockBullet();
        indexWeapon = 2;
        PlayerPrefs.SetInt("indexWeapon", indexWeapon);
    }

    public void SetMkBullets()
    {
        bullets[0].LockBullet();
        bullets[2].LockBullet();
        bullets[1].UnlockBullet();
        weapons[0].SetActive(false);
        weapons[1].SetActive(true);
        weapons[2].SetActive(false);
        indexWeapon = 1;
        PlayerPrefs.SetInt("indexWeapon", indexWeapon);
    }

}

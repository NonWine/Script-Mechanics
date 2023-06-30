using UnityEngine;
using NTC.Global.Pool;

public class RifleBullets : Bullet
{
    [SerializeField] private Transform[] riflePosThrow;

    private void Start()
    {
        isOpen = PlayerPrefsExtra.GetBool("RifleBullet", isOpen);
        _throwCD = PlayerPrefs.GetFloat("throwCDDefaultBullets", _throwCD);
        _damage = PlayerPrefs.GetInt("DefaultBulletDamage", _damage);
        isCrit = PlayerPrefsExtra.GetBool("ShotGunCrit", isCrit);
        startDamage = _damage;
    }

    public override void AddDamage(int value)
    {
        base.AddDamage(value);
        PlayerPrefs.SetInt("DefaultBulletDamage", _damage);
        startDamage = _damage;
       // DamageController.Instance.PlayerChangeSlider();

    }

    public override void ReduceThrowCD(float value)
    {
        base.ReduceThrowCD(value);
        PlayerPrefs.SetFloat("throwCDDefaultBullets", _throwCD);

    }


    public override void UnlockCritDamage()
    {
        base.UnlockCritDamage();
        PlayerPrefsExtra.SetBool("ShotGunCrit", isCrit);
    }


    public override void Draw(Unit unit)
    {
        float heightK;
        if (unit.GetType() == Mob.Mimic)
            heightK = 5f;
        else
            heightK = 1f;
        float offset = -2f;
        for (int i = 0; i < riflePosThrow.Length; i++)
        {
            GameObject prefabArrow = NightPool.Spawn(bullet, riflePosThrow[i].position + new Vector3(0f,heightK,0f), Quaternion.identity);
            Projectile CurrentArrow = prefabArrow.GetComponent<Projectile>();
            CurrentArrow.SetDamage(_damage);
            //if (isCrit)
            //{

            //    CurrentArrow.SetCrit();
            //    CurrentArrow.SetCrtiChance(_critChance);
            //    CurrentArrow.SetCritDamage(_criDamage);
            //}
            Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
            bulletBody.velocity = riflePosThrow[i].forward  * _speed;
            offset += 1f;
        }
    }

    public override void LockBullet()
    {
        base.LockBullet();
        PlayerPrefsExtra.SetBool("RifleBullet", isOpen);
    }

    public override void UnlockBullet()
    {
        base.UnlockBullet();

        PlayerPrefsExtra.SetBool("RifleBullet", isOpen);
    }
}

using UnityEngine;

public class PistolBullets : Bullet
{
    private void Start()
    {
        isOpen = PlayerPrefsExtra.GetBool("DefaultBullet", true);
        _throwCD = PlayerPrefs.GetFloat("throwCDDefaultBullets", _throwCD);
        _damage = PlayerPrefs.GetInt("DefaultBulletDamage", _damage);
        isCrit = PlayerPrefsExtra.GetBool("PistolCrit", isCrit);
        startDamage = _damage;
    }

    public override void UnlockCritDamage()
    {
        base.UnlockCritDamage();
        PlayerPrefsExtra.SetBool("PistolCrit", isCrit);
    }

    public override void AddDamage(int value)
    {
        base.AddDamage(value);
        PlayerPrefs.SetInt("DefaultBulletDamage", _damage);
        startDamage = _damage;
      //  DamageController.Instance.PlayerChangeSlider();
    }

    public override void LockBullet()
    {
        base.LockBullet();
        PlayerPrefsExtra.SetBool("DefaultBullet", isOpen);
    }

    public override void UnlockBullet()
    {
        base.UnlockBullet();

        PlayerPrefsExtra.SetBool("DefaultBullet", isOpen);
    }

    public override void ReduceThrowCD(float value)
    {
        base.ReduceThrowCD(value);
        PlayerPrefs.SetFloat("throwCDDefaultBullets", _throwCD);

    }

    public void SetCritDamage() { bullet.GetComponent<Projectile>().SetCrit(); }

    
}

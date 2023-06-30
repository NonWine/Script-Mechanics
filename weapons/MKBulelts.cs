using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKBulelts : Bullet
{
    private void Start()
    {
        isOpen = PlayerPrefsExtra.GetBool("MKBullet", isOpen);
        _throwCD = PlayerPrefs.GetFloat("throwCDMKBullet", _throwCD);
        _damage = PlayerPrefs.GetInt("DefaultBulletMKBullet", _damage);
        isCrit = PlayerPrefsExtra.GetBool("AKCrit", isCrit);
        startDamage = _damage;
    }

    public override void AddDamage(int value)
    {
        base.AddDamage(value);
        PlayerPrefs.SetInt("DefaultBulletMKBullet", _damage);
        startDamage = _damage;
   //     DamageController.Instance.PlayerChangeSlider();

    }

    public override void ReduceThrowCD(float value)
    {
        base.ReduceThrowCD(value);
        PlayerPrefs.SetFloat("throwCDMKBullet", _throwCD);

    }

    public override void UnlockCritDamage()
    {
        base.UnlockCritDamage();
        PlayerPrefsExtra.SetBool("AKCrit", isCrit);
    }

    public override void LockBullet()
    {
        base.LockBullet();
        PlayerPrefsExtra.SetBool("MKBullet", isOpen);
    }

    public override void UnlockBullet()
    {
        base.UnlockBullet();

        PlayerPrefsExtra.SetBool("MKBullet", isOpen);
    }
}

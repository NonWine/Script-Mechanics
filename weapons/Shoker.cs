using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoker : Bullet
{
    [SerializeField] private int maxRc;
    [SerializeField] private int _radius;

    private void Awake()
    {
        maxRc = PlayerPrefs.GetInt("maxRc", maxRc);
        isOpen = PlayerPrefsExtra.GetBool("isOpenShoker", isOpen);
        _damage = PlayerPrefs.GetInt("ShokerDamage", _damage);
        _throwCD = PlayerPrefs.GetFloat("ShockerCD", _throwCD);
    }

    public override void Draw(Unit unit)
    {
        float heightK;
        if (unit.GetType() == Mob.Mimic)
            heightK = 8f;
        else
            heightK = 4f;
        GameObject prefabArrow = Instantiate(bullet, posThrow.position, Player.Instance.gameObject.GetComponent<PlayerController>().GetBody().rotation);
        ShokerTile CurrentArrow = prefabArrow.GetComponent<ShokerTile>();
        CurrentArrow.SetDamage(_damage);
        CurrentArrow.SetMaxRc(maxRc);
        CurrentArrow.SetRadius(_radius);
        CurrentArrow.SetSpeed(_speed);
        Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
        bulletBody.velocity = ((unit.transform.position + (Vector3.up * heightK)) - prefabArrow.transform.position).normalized * _speed;

    }

    public int GetRadius() { return _radius; }

    public void IncreaseMaxRd()
    {
        maxRc++;
        PlayerPrefs.SetInt("maxRc", maxRc);
    }

    public override void AddDamage(int value)
    {
        base.AddDamage(value);
        PlayerPrefs.SetInt("ShokerDamage", _damage);
    }

    public override void ReduceThrowCD(float value)
    {
        base.ReduceThrowCD(value);
        PlayerPrefs.SetFloat("ShockerCD", _throwCD);
    }

    public override void UnlockBullet()
    {
        base.UnlockBullet();
        PlayerPrefsExtra.SetBool("isOpenShoker", isOpen);
    }
}

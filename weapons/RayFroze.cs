using UnityEngine;
using NTC.Global.Pool;
public class RayFroze : Bullet
{
    [SerializeField] private float frostTime;
    private void Awake()
    {
        frostTime = PlayerPrefs.GetFloat("RayFrostTime", frostTime);
        _throwCD = PlayerPrefs.GetFloat("frozenTimer", _throwCD);
        isOpen = PlayerPrefsExtra.GetBool("FrozeOpen", isOpen);
        _damage = PlayerPrefs.GetInt("RayDamage", _damage);
    }

    public override void Draw(Unit unit)
    {
        float heightK;
        if (unit.GetType() == Mob.Mimic)
            heightK = 8f;
        else
            heightK = 4f;
        GameObject prefabArrow = NightPool.Spawn(bullet, posThrow.position, Player.Instance.gameObject.GetComponent<PlayerController>().GetBody().rotation);
        Projectile CurrentArrow = prefabArrow.GetComponent<Projectile>();
        prefabArrow.GetComponent<Explosive>().SetFrostTime(frostTime);
        CurrentArrow.SetDamage(_damage);
        if (isCrit)
            CurrentArrow.GetComponent<Projectile>().SetCrit();
        Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
        bulletBody.velocity = ((unit.transform.position + (Vector3.up * heightK)) - prefabArrow.transform.position).normalized * _speed;
    }

    public override void ReduceThrowCD(float value)
    {
        _throwCD -= value;
        PlayerPrefs.SetFloat("frozenTimer", _throwCD);
    }

    public void IncreaseFrostTime(float value)
    {
        frostTime += value;
        PlayerPrefs.SetFloat("RayFrostTime", frostTime);
    }

    public override void AddDamage(int value)
    {
        base.AddDamage(value);
        PlayerPrefs.SetInt("RayDamage", _damage);
    }

    public override void UnlockBullet()
    {
        base.UnlockBullet();
        PlayerPrefsExtra.SetBool("FrozeOpen", isOpen);
    }

    
}

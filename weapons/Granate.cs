using UnityEngine;
using NTC.Global.Pool;
public class Granate : Bullet
{
    [SerializeField] private float radius;

    private void Awake()
    {
        _damage = PlayerPrefs.GetInt("GranateDamage", _damage);
        isOpen = PlayerPrefsExtra.GetBool("GrenadeOpen", isOpen);
        _speed = PlayerPrefs.GetFloat("GrenadeSpeed", _speed);
        radius = PlayerPrefs.GetFloat("GrenadeRadius", radius);
      //  isOpen = true;
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
        CurrentArrow.SetDamage(_damage);
        prefabArrow.GetComponent<Explosive>().SetRadius(radius);
        prefabArrow.GetComponent<Explosive>().SetExploseDamage(_damage);
        if (isCrit)
            CurrentArrow.GetComponent<Projectile>().SetCrit();
        Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
        bulletBody.velocity = ((unit.transform.position + (Vector3.up * heightK)) - prefabArrow.transform.position).normalized * _speed;
    }

    public override void AddDamage(int value)
    {
        base.AddDamage(value);
        PlayerPrefs.GetInt("GranateDamage", _damage);
    }

    public override void AddSpeed(int value)
    {
        base.AddSpeed(value);
        PlayerPrefs.SetFloat("GrenadeSpeed", _speed);
    }

    public void InreaseRadius(float value)
    {
        radius += value;
        PlayerPrefs.SetFloat("GrenadeRadius", radius);
    }

    public override void UnlockBullet()
    {
        base.UnlockBullet();
      
        PlayerPrefsExtra.SetBool("GrenadeOpen", isOpen);
    }

    public float GetRadius() { return radius; }

}

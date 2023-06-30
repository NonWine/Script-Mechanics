using NTC.Global.Pool;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected Transform posThrow;
    [SerializeField] protected float _throwCD;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed = 80f;
    [Header("CritS For Weapons")]
    [SerializeField] protected int _critChance;
    [SerializeField] protected int _criDamage;
    protected float timer;
    protected bool isOpen;
    protected bool isCrit;
    protected int level;
    protected int startDamage;
    private void Awake()
    {
       _criDamage =  PlayerPrefs.GetInt("critDamage", _criDamage);
       _critChance = PlayerPrefs.GetInt("critChance", _critChance);

    }

    public virtual void Draw(Unit unit)
    {
        float heightK;
        if (unit.GetType() == Mob.Mimic)
            heightK = 8f;
        else
            heightK = 3f;
        GameObject prefabArrow = Instantiate(bullet, posThrow.position, Player.Instance.gameObject.GetComponent<PlayerController>().GetBody().rotation);
        Projectile CurrentArrow = prefabArrow.GetComponent<Projectile>();
        CurrentArrow.SetDamage(_damage);
        if (isCrit)
        {

            CurrentArrow.SetCrit();
            CurrentArrow.SetCrtiChance(_critChance);
            CurrentArrow.SetCritDamage(_criDamage);
        }
        
        Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
        bulletBody.velocity = ((unit.transform.position + (Vector3.up * heightK)) - prefabArrow.transform.position).normalized * _speed;
    }

    public virtual void Spam() { }

    public void IncreaseCritDamage(int value)
    {
        _criDamage += value;
        PlayerPrefs.SetInt("critDamage", _criDamage);
    }

    public void IncreaseCritChance(int value)
    {
        _critChance += value;
        PlayerPrefs.SetInt("critChance", _critChance);

    }

    public  bool isReload()
    {
        if (timer >= _throwCD && isOpen)
        {
            timer = 0f;
            return true;
        }
        else
            return false;
    }

    public virtual void UnlockBullet()
    {
        isOpen = true;
    }

    public virtual void LockBullet()
    {
        isOpen = false;
    }

    public virtual void UnlockCritDamage()
    {
        isCrit = true;
      
    }

    public virtual void ReduceThrowCD(float value) => _throwCD -= value;

    public virtual void AddDamage(int value) => _damage += value;

    public virtual void AddSpeed(int value) => _speed += value;

    public float GetThromCD() { return _throwCD; }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void SetDamage(int t) => _damage = t;

    public int GetDamage() { return startDamage; }
}

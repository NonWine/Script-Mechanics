using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBullets : Bullet
{
    [SerializeField] private int maxCount;
    private bool isEndedQueue;
    private int currentCount;

    private void Awake()
    {
        isOpen = true;
        _damage = PlayerPrefs.GetInt("DroneBulletDamage", _damage);
        maxCount = PlayerPrefs.GetInt("DroneMaxCount", maxCount);
    }

    public override void Draw(Unit unit)
    {

        float heightK;
        if (unit.GetType() == Mob.Mimic)
            heightK = 8f;
        else
            heightK = 3f;
        GameObject prefabArrow = Instantiate(bullet, posThrow.position, posThrow.localRotation);
        Projectile CurrentArrow = prefabArrow.GetComponent<Projectile>();
        CurrentArrow.SetDamage(_damage);
        Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
        bulletBody.velocity = ((unit.transform.position + (Vector3.up * heightK)) - prefabArrow.transform.position).normalized * _speed;
        prefabArrow.transform.LookAt(unit.transform);
        currentCount++;
        if (currentCount >= maxCount) 
        {
            isEndedQueue = true;
            currentCount = 0;
        }
               
        
      
    }

    public void IncreaseMaxCountInQueue(int value)
    {
        maxCount += value;
        PlayerPrefs.SetInt("DroneMaxCount", maxCount);
    }

    public override void AddDamage(int value)
    {
        base.AddDamage(value);
        PlayerPrefs.SetInt("DroneBulletDamage", _damage);
    }

    public void SetBulletPos(Transform point)
    {
        posThrow = point;
    }

    public bool GetQueue() { return isEndedQueue; }

    public void SetStartQueue() => isEndedQueue = false;
}

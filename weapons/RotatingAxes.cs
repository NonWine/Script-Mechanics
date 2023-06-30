using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAxes : Bullet
{
    [SerializeField] private Transform[] axesPos;
    [SerializeField] private float _speedRotation;
    [SerializeField] private float timeTolive;
    [SerializeField] private float _distanceForce;
    private int maxCount = 1;
    private bool isForever;

    private void Start()
    {
        isOpen = PlayerPrefsExtra.GetBool("AxesOpen", isOpen);
        _throwCD = PlayerPrefs.GetFloat("SawCoolDown", _throwCD);
        timeTolive = PlayerPrefs.GetFloat("TimeToLiveAxes", timeTolive);
        maxCount = PlayerPrefs.GetInt("maxCountAxes", maxCount);
        isForever = PlayerPrefsExtra.GetBool("isForever", isForever);
        _damage = PlayerPrefs.GetInt("AxesDamage", _damage);
      //  if(!isForever)
    //    _throwCD += timeTolive;
     //   isOpen = true;
    }

    public override void Spam()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject Axe = Instantiate(bullet, axesPos[i].position, Quaternion.Euler(new Vector3(90f,0f,0f)));
            Axe.transform.SetParent(Player.Instance.gameObject.transform);
            RotatingObject rotatingObject = Axe.GetComponent<RotatingObject>();
            rotatingObject.SetDamage(_damage);
            rotatingObject.SetSpeedRotation(_speedRotation);
            rotatingObject.SetTimeToLive(timeTolive);
            
        }
    }

    public override void ReduceThrowCD(float value)
    {
        base.ReduceThrowCD(value);
        PlayerPrefs.SetFloat("SawCoolDown", _throwCD);
    }

    public override void AddDamage(int value)
    {
        base.AddDamage(value);
        PlayerPrefs.SetInt("AxesDamage", _damage);
    }

    public void AddTimeToLive(float value)
    {
        timeTolive += value;
      //  _throwCD += timeTolive;
      //  PlayerPrefs.GetFloat("AxesCD", _throwCD);
        PlayerPrefs.SetFloat("TimeToLiveAxes", timeTolive);
    }

    public override void UnlockBullet()
    {
        base.UnlockBullet();
        PlayerPrefsExtra.SetBool("AxesOpen", isOpen);
    }

    public void AddCount()
    {
        maxCount++;
        PlayerPrefs.SetInt("maxCountAxes", maxCount);
    }

    public void SetForever()
    {
        isForever = true;
        PlayerPrefsExtra.SetBool("isForever", isForever);
    }

    public int GetLevel() { return level; }
}

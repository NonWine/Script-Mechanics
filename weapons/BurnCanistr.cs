using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnCanistr : Bullet
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float _perSecDamage;
    [SerializeField] private float _timeToliveField;
    [SerializeField] private float radius;
    private List<Transform> bisyPoints = new List<Transform>();
    private int MaxCount = 1;

    private void Awake()
    {

        MaxCount = PlayerPrefs.GetInt("maxCountCanistr", MaxCount);
        _damage = PlayerPrefs.GetInt("CanistrDamage", _damage);
        radius = PlayerPrefs.GetFloat("CanistrRadius", radius);
        isOpen = PlayerPrefsExtra.GetBool("IsOpenCanistr", isOpen);
        _throwCD = PlayerPrefs.GetFloat("canistrCD", _throwCD);
     //  isOpen = true;
    }



    public override void Spam()
    {
        for (int i = 0; i < MaxCount; i++)
        {
            GameObject benzin = Instantiate(bullet, posThrow.position, posThrow.rotation);
            CanistraBullet canistra = benzin.GetComponent<CanistraBullet>();
            canistra.SetPerSec(_perSecDamage);
            canistra.SetDamage(_damage);
            canistra.SetTimeToLive(_timeToliveField);
            canistra.SetRadius(radius);
            Rigidbody benzinBody = benzin.GetComponent<Rigidbody>();
            benzinBody.velocity = (DefinePoint().position - benzin.transform.position).normalized * _speed;
            Debug.Log("spawnCAnistr");
        }
        bisyPoints.Clear();
    }

    private Transform DefinePoint()
    {
        bool isFind = true;
        Transform newPoint = points[Random.Range(0, points.Length)];
        if(bisyPoints.Count >= 1)
        while (true)
        {
            newPoint = points[Random.Range(0, points.Length)];
            foreach (var item in bisyPoints)
            {
                if (item.transform == newPoint.transform)
                {
                    Debug.Log("find" + item.transform);
                    isFind = true;
                    break;
                }
                else
                {
                    isFind = false;
                }
                    
            }
            if (isFind == false)
                break;
        }
        bisyPoints.Add(newPoint);
        return newPoint;
    }

    public float GetRadius() { return radius; }

    public void IncreaseCountCanistr()
    {
        MaxCount++;
        PlayerPrefs.SetInt("maxCountCanistr", MaxCount);
    }

    public override void ReduceThrowCD(float value)
    {
        base.ReduceThrowCD(value);
        PlayerPrefs.SetFloat("canistrCD", _throwCD);
    }

    public void IncreaseRadius(float value)
    {
        Debug.Log(value);
        radius += value;
        PlayerPrefs.SetFloat("CanistrRadius", radius);
    }

    public override void AddDamage(int value)
    {
        base.AddDamage(value);
        PlayerPrefs.SetInt("CanistrDamage", _damage);
    }

    public override void UnlockBullet()
    {
        base.UnlockBullet();
        PlayerPrefsExtra.SetBool("IsOpenCanistr", isOpen);
    }
}

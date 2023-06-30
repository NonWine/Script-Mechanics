using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicField : Bullet
{
    [SerializeField] private float _perSecDamage;
    [SerializeField] private float _timeToliveField = Mathf.Infinity;
    [SerializeField] private float radius;
    private bool isThrow;
    private Field field2;
    private void Awake()
    {
      //  isOpen = true;
      
       _throwCD = PlayerPrefs.GetFloat("MagicFieldReduceCD", _throwCD);
        _damage = PlayerPrefs.GetInt("FieldDamage", _damage);
        _damage += 40;
        radius = PlayerPrefs.GetFloat("FieldRadius", radius);
        isOpen = PlayerPrefsExtra.GetBool("fieldisOpen", isOpen);
    }


    public override void Spam()
    {
        if (!isThrow)
        {
            GameObject field = Instantiate(bullet, posThrow.position, Quaternion.identity);
            field.transform.SetParent(posThrow);
            field2  = field.GetComponent<Field>();
            field2.SetMagicField();
            field2.SetRadius(radius);
            field2.SetDamage(_damage);
            field2.SetPerSec(_perSecDamage);
            field2.SetTimeToLive(_timeToliveField);
            isThrow = true;
        }

    }

    public void IncreaseRadius(float value)
    {
        Debug.Log(value);
        radius += value;
        if(field2 != null)
        field2.SetRadius(radius);
        PlayerPrefs.SetFloat("FieldRadius", radius);
    }

    public override void AddDamage(int value)
    {
        base.AddDamage(value);
        if(field2 != null)
        field2.SetDamage(_damage);
        PlayerPrefs.SetInt("FieldDamage", _damage);
    }

    public override void ReduceThrowCD(float value)
    {
        base.ReduceThrowCD(value);
        PlayerPrefs.SetFloat("MagicFieldReduceCD", _throwCD);
    }

    public override void UnlockBullet()
    {
        base.UnlockBullet();
        PlayerPrefsExtra.SetBool("fieldisOpen", isOpen);
    }

    public float GetRadius() { return radius; }
}

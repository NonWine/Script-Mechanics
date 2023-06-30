using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    private float speedRotation;
    private int damage;
    private float timeTolive;
    private float force;
    private bool trig;
    private float timer;
    private void Start()
    {
        transform.localScale = Vector3.zero;
        Destroy(gameObject, timeTolive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dire"))
        {
            Unit unit = other.GetComponent<Unit>();
            unit.GetDamage(damage);
            if (unit.GetType() != Mob.Mimic)
                other.GetComponent<ControllStateUnit>().ForceState();
            else
                unit.gameObject.GetComponent<MimicSpace.Movement>().ForceState();
        }
    }

    
    private void Update()
    {
        transform.Rotate(Vector3.forward * 1000f * Time.deltaTime);
        transform.RotateAround(transform.parent.transform.position, Vector3.up, speedRotation * Time.deltaTime);
        if (!trig)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(100f, 100f, 100f), 6 * timer);
            if (transform.localScale == new Vector3(100f, 100f, 100f))
                trig = true;
        }


    }

    public void SetForce(float value) => force = value;

    public void SetSpeedRotation(float value) => speedRotation = value;
    
    public void SetTimeToLive(float time) => timeTolive = time;
    
    public void SetDamage(int value) => damage = value;
}

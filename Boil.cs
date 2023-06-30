using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MimicSpace;
public class Boil : MonoBehaviour
{
    public static Boil Instance { get; private set; } 
    private  int time;
     private  int reduceSpeed;
    private List<Unit> units = new List<Unit>();

    private void Start()
    {
        Invoke(nameof(DeActiveWater), time - 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dire"))
        {
            Unit unit = other.GetComponent<Unit>();
            units.Add(unit);
            if (unit.GetType() == Mob.Mimic)
            {
                unit.GetComponent<Movement>().GetNavMesh().speed -= reduceSpeed;
            }
            else
                other.gameObject.GetComponent<ControllStateUnit>().BoilState(reduceSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dire"))
        {
            Unit unit = other.GetComponent<Unit>();
            if (unit.GetType() == Mob.Mimic)
            {
                unit.GetComponent<Movement>().GetNavMesh().speed += reduceSpeed;
            }
            else
                other.gameObject.GetComponent<ControllStateUnit>().UnBoilState();
        }
    }

    private void DeActiveWater()
    {
        foreach (var item in units)
        {
            if (item != null)
            {
                if (item.GetType() == Mob.Mimic)
                {
                    item.GetComponent<Movement>().GetNavMesh().speed += reduceSpeed;
                }
                else
                    item.GetState().UnBoilState();
            }

        }
        Destroy(gameObject, 0.5f);
    }

    public int GetSpeed() { return reduceSpeed; }
    public  void SetReduceSpeed(int value)
    {
        reduceSpeed = value;
    }

    public  void SetTimeDuration(int value)
    {
        time = value;
    }
}
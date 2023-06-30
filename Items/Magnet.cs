using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Item
{
    protected override void Action()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 400);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Coin"))
            {
                colliders[i].GetComponent<Coin>().SetRadius(500);
            }
        }
        Spawn();
    }

    protected override IEnumerator Respawn()
    {
        UnitManager.Instance.haveMagnet = false;
        Destroy(gameObject);
        yield break;
    }
}

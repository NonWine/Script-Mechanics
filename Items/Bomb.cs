using UnityEngine;
using System.Collections;

public class Bomb : Item
{
    protected override void Action()
    {
        ParticlePool.Instance.PlayBombFx(transform.position + Vector3.up);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 400);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Dire"))
            {
                Unit unit = colliders[i].GetComponent<Unit>();
                if(unit.GetType() != Mob.BigRadioActive && unit.GetType() != Mob.BigAlien && unit.GetType() != Mob.BigPolzun && unit.GetType() != Mob.BigSamurai && unit.GetType() != Mob.BigSamuraiType2 && unit.GetType() != Mob.FinalSamurai)
                    unit.Death();
            }
        }
        Spawn();
    }

    protected override IEnumerator Respawn()
    {
        UnitManager.Instance.haveBomb = false;
        Destroy(gameObject);
        yield break;
    }
}

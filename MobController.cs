using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour, IControllable
{
    private NavMeshAgent myAgent;
   [SerializeField] private Animator myAnim;
    private bool dead;
    private void Start()
    {
      
        myAgent = GetComponent<NavMeshAgent>();
    }

    public void Move(Transform target, Unit unit)
    {
        if (Vector3.Distance(transform.position, target.position) <= unit.GetDistance())
            unit.SetAttacking(true);

        if (unit.GetAttack() == false)
        {
            myAnim.SetInteger("state", 1);
            myAnim.SetLayerWeight(1, 0f);
            myAgent.isStopped = false;
        }
        else if (unit.GetAttack())
        {
            myAgent.isStopped = true;
            myAgent.velocity = Vector3.zero;

            unit.GiveDamage();
        }
        myAgent.SetDestination(target.position);
    }

    public void Death()
    {
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Unit>().SetDeath();
        myAgent.isStopped = true;
        myAgent.velocity = Vector3.zero;
        myAgent.enabled = false;
        
        dead = true;
        ParticlePool.Instance.PlayDeadZombie(transform.position);
        myAnim.SetLayerWeight(1, 0f);
        myAnim.SetInteger("state", 3);
        GetComponent<HealthUI>().TurnOffUiHP();
        SpawnManager.Instance.AddDeathEnemy();
        GetComponent<Unit>().SpawnCoins();
        Invoke(nameof(DisableMee), 2f);

    }

    private void DisableMee()
    {
        // Destroy(gameObject);
        Unit unit = GetComponent<Unit>();
        if (unit.GetType() != Mob.BigAlianType2 && unit.GetType() != Mob.BigPolzun && unit.GetType()
            != Mob.BigRadioActive && unit.GetType() != Mob.BigSamurai && unit.GetType() != Mob.BigAlien && unit.GetType()
            != Mob.FinalSamurai && unit.GetType() != Mob.BigRadioActiveType2 && unit.GetType() != Mob.BigPolzunType2 && unit.GetType() != Mob.BigSamuraiType2)
            GetComponent<Unit>().getPool().Release(GetComponent<Unit>());
        else
            Destroy(gameObject);
    }

    public bool isDeath()
    {
        return dead;
    }
}

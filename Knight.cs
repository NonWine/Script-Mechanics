using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using NTC.Global.Cache;
using NTC.Global.Pool;
public class Knight : MonoCache
{
    [SerializeField] private MobStats stats;
    [SerializeField] private GameObject body;
    [SerializeField] private int _distanceToPlayer;
    [SerializeField] private int _distanceToEnemy;
    [SerializeField] private Detector _detector;
    [SerializeField] private int timeToRespawn;
    [SerializeField] private Animator myAnim;
    private NavMeshAgent _agent;
    private float timer, stopTimer;
    private float cd;
    private bool dead;
    private Transform target;

    private void Start()
    {
  
        stats.SetDefaultKnight();
        _agent = GetComponent<NavMeshAgent>();
        cd = stats.GetCD();
        
    }

    protected override void Run()
    {
        FindClosetsTarget();
        if(target != null && !dead)
            Move(target);
        //if (_agent.isStopped == true)
        //{
        //   // stopTimer += Time.deltaTime;
        //    _agent.velocity = Vector3.Lerp(_agent.velocity, Vector3.zero, stopTimer);
        //    if (_agent.velocity == Vector3.zero)
        //        stopTimer = 0f;
        //}
    }

    private void Move(Transform target)
    {
        if (target.CompareTag("Player"))
        {
            FollowToPlayer(target);
        }
        else
            FollowToEnemy(target);
      
        
    }

    private void FindClosetsTarget()
    {
        _detector.setPointInfinity();
        _detector.TryFindTheNearlestEnemy(transform.position);
        if (_detector.isDetected())
            target = _detector.GetTargetInfo();
        else
            target = Player.Instance.transform;

    }


    private void FollowToEnemy(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) < _distanceToEnemy)
        {
          
            myAnim.SetInteger("state", 0);
            _agent.velocity = Vector3.zero;
            GiveDamage(target.GetComponent<Unit>());
           
        }
        else
        {
            myAnim.SetInteger("state", 1);
            _agent.isStopped = false;
            _agent.SetDestination(target.position);
        }
    }

    

    private void FollowToPlayer(Transform target)
    {

        if (Vector3.Distance(Player.Instance.transform.position, transform.position) <= _distanceToPlayer)
        {
  
            myAnim.SetInteger("state", 0);
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
        }
        else
        {
            myAnim.SetInteger("state", 1);
            _agent.isStopped = false;
            stopTimer = 0f;
            _agent.SetDestination(target.position);
        }

    }

    public void GiveDamage(Unit enemy)
    {
        if (!enemy.isDead())
        {
            timer += Time.deltaTime;
            myAnim.SetInteger("state", 2);
            if (timer >= cd)
            {
                enemy.GetDamage(stats.GetDamage());
                timer = 0f;
            }

        }
        else myAnim.SetInteger("state", 0);

    }


    
    private  IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1.2f);
        body.SetActive(false);
        yield return new WaitForSeconds(timeToRespawn);
        UnitManager.Instance.RespawnKnight(false);
       Destroy(gameObject);
    }


    private void Death()
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
        _agent.enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        dead = true;
        myAnim.SetInteger("state", 3);
        transform.position = new Vector3(transform.position.x, -1.5f, transform.position.z);
        StartCoroutine(Respawn());
    }

    public void GetDamage(int dmg)
    {
        stats.ReduceHealth(dmg);

        ParticlePool.Instance.PlayBlood(transform.position);
        if (stats.GetHealth() <= 0)
            Death();
    }

    public bool isDead() { return dead; }
}
using UnityEngine;
using UnityEngine.AI;
using NTC.Global.Cache;
using System.Collections;
public class SLowerMan : MonoBehaviour
{
    public static SLowerMan Instance { get; private set; }
    [SerializeField] private GameObject body;
    [SerializeField] private int _distanceToPlayer;
    [SerializeField] private int _distanceToEnemy;
    [SerializeField] private Detector _detector;
    [SerializeField] private float timeToRespawn;
    [SerializeField] private Animator myAnim;
    [SerializeField] private GameObject boil;
    [SerializeField] private int radiusDetect;
    [SerializeField] private Transform spiderPoint;
    [SerializeField] private int time;
    [SerializeField] private int reduceSpeed;
    [SerializeField] private int damageExplose;
    private NavMeshAgent _agent;
    private Transform target;
    private bool dead;
    private int currentLevel;

    private void Awake()
    {
        Instance = this;
        _detector.SetRadius(radiusDetect);
        timeToRespawn = PlayerPrefs.GetFloat("timeToRespawnSpider",timeToRespawn);
        currentLevel = PlayerPrefs.GetInt("currentSpiderLevel", currentLevel);
        reduceSpeed = PlayerPrefs.GetInt("BoilSpeed", reduceSpeed);
        time = PlayerPrefs.GetInt("time", time);
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        FindClosetsTarget();
        if(!dead)
            Move();
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

    private void Move()
    {
        if (_detector.isDetected() && !dead)
        {
            FollowToEnemy();
        }
        else
            FollowToPlayer();


    }

    private void FollowToEnemy()
    {
        if (Vector3.Distance(transform.position, target.position) < _distanceToEnemy)
        {
            Death();
            _agent.velocity = Vector3.zero;
            myAnim.SetInteger("state", 0);
 

        }
        else
        {
            myAnim.SetInteger("state", 1);
            _agent.isStopped = false;
            _agent.SetDestination(target.position);
        }
    }



    private void FollowToPlayer()
    {

        if (Vector3.Distance(spiderPoint.transform.position, transform.position) <= _distanceToPlayer)
        {
            myAnim.SetInteger("state", 0);
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
        }
        else
        {
            myAnim.SetInteger("state", 1);
            _agent.isStopped = false;
            _agent.SetDestination(spiderPoint.position);
        }

    }


    public void Death()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 13);
        foreach (var item in enemies)
        {
            if (item.CompareTag("Dire"))
            {
                Unit enemy = item.GetComponent<Unit>();
                enemy.GetDamage(damageExplose);

            }
        }
        dead = true;
        _agent.enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        ParticlePool.Instance.PlayExplosiveSmola(transform.position);
        GameObject obj =  Instantiate(boil, transform.position + Vector3.up, Quaternion.identity);
        Boil prefabBoil = obj.GetComponent<Boil>();
        prefabBoil.SetReduceSpeed(reduceSpeed);
        prefabBoil.SetTimeDuration(time);
        
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        body.SetActive(false);
        yield return new WaitForSeconds(timeToRespawn);
        body.SetActive(true);
        dead = false;
        transform.position = Player.Instance.transform.position;
        _agent.enabled = true;
        GetComponent<SphereCollider>().enabled = true;
    }

    public void IncreaseReduceSpeed(int value) 
    {
        reduceSpeed += value;
        PlayerPrefs.SetInt("BoilSpeed", reduceSpeed);
    } 
    public void ReduceTimeToRespawn(float value) {
        timeToRespawn -= value;
        PlayerPrefs.SetFloat("timeToRespawnSpider", timeToRespawn);
    }

    public void IncreaseTimeDuration(int value)
    {
        time += value;
        PlayerPrefs.SetInt("time", time);
    }

    public void SetDamageExplose(int value)
    {
        damageExplose = value;
    }
}

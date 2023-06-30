using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using NTC.Global.Cache;
using NTC.Global.Pool;
public class Drone : MonoBehaviour
{
    [SerializeField] private GameObject body;
    [SerializeField] private int _distanceToPlayer;
    private int _distanceToEnemy;
    [SerializeField] private Detector _detector;
    [SerializeField] private Animator myAnim;
    [SerializeField] private float ShootCD;
    [SerializeField] private DroneBullets bullet;
    [SerializeField] private Transform dronePoint;
    [SerializeField] private int radiusDetect;
    [SerializeField] private Transform[] wixlopi;
    private bool dead;
    private Transform target;
    private NavMeshAgent _agent;
    private float timer;
    private bool attack;
    private void Awake()
    {
      radiusDetect =   PlayerPrefs.GetInt("DronRadius", radiusDetect);
      ShootCD =   PlayerPrefs.GetFloat("ShootCDDrone", ShootCD);

    }

    private void Start()
    {
        _detector.SetRadius(radiusDetect);
        _agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        FindClosetsTarget();
            Move();
        foreach (var item in wixlopi)
        {
            item.Rotate(Vector3.forward * 1000f * Time.deltaTime);
        }
    //    Debug.Log(timer);
    }

    private void Move()
    {
        FollowToPlayer();
        if (_detector.isDetected())
        {
            GiveDamage(target.GetComponent<Unit>());
            attack = true;
        }
        else
            attack = false;
        
    }

    private void FindClosetsTarget()
    {
        _detector.setPointInfinity();
        _detector.TryFindTheNearlestEnemy(transform.position);
        if (_detector.isDetected())
            target = _detector.GetTargetInfo();
    }
    
    private void FollowToPlayer()
    {

        if (Vector3.Distance(dronePoint.transform.position, transform.position) <= _distanceToPlayer)
        {
  
       //     myAnim.SetInteger("state", 0);
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
        }
        else
        {
       //     myAnim.SetInteger("state", 1);
            _agent.isStopped = false;
         
            _agent.SetDestination(dronePoint.transform.position);
        }

    }

    public void GiveDamage(Unit enemy)
    {
        if (!enemy.isDead() && attack)
        {
          
            if (timer >= ShootCD)
            {
               if (bullet.isReload())
                  {
                     
                     bullet.Draw(enemy);
                  }
                if (bullet.GetQueue())
                {
                    timer = 0f;
                    bullet.SetStartQueue();
                }
                 
            }

        }
    }

    public void IncreaseRadius(int value)
    {
        radiusDetect += value;
        PlayerPrefs.SetInt("DronRadius", radiusDetect);
        _detector.SetRadius(radiusDetect);
    }
    public void IncreaseDamage(int value)
    {
        bullet.AddDamage(value);
    }
    public void ReduceCD(float value)
    {
        ShootCD -= value;
        PlayerPrefs.SetFloat("ShootCDDrone", ShootCD);
    }
}
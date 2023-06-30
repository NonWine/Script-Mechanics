using UnityEngine;
using UnityEngine.AI;
using NTC.Global.Cache;
using NTC.Global.Pool;
using MimicSpace;
using System.Collections;
using UnityEngine.Pool;
public class Unit : MonoCache
{
    [SerializeField] private Mob type;
    [SerializeField] private MobStats stats;
    [SerializeField] private int _rewardMoney;
    [SerializeField] private int bigCoinCount;
    [SerializeField] private int midleCoin;
    [SerializeField] private int boosCoinCount;
    [SerializeField] private int _distance;
    [SerializeField] private Animator myAnim;
    [SerializeField] private Detector _detector;
    [SerializeField] private Coin coin;
    //   private
    private ControllStateUnit stateUnit;
    private NavMeshAgent myAgent;
    private HealthUI healthUI;
    private Transform target;
    private bool dead, attaking, canMove;
    private float timer;
    private IControllable unitMove;
    private IObjectPool<Unit> pool;
    private int startDamage;
    private int settedDam;

    private void Start()
    {
        SetStats();
        canMove = true;
        healthUI = gameObject.GetComponent<HealthUI>();
        stateUnit = GetComponent<ControllStateUnit>();
        myAgent = GetComponent<NavMeshAgent>();
        healthUI.SetHealth(stats.GetHealth());
        myAgent.speed = stats.GetSpeed();
        MobList.AddMobInList(this);
        unitMove = GetComponent<IControllable>();
        startDamage = stats.GetDamage();
        Debug.Log(startDamage);
        startDamage = (int)((float)(startDamage / 100f) * (85f));
        stats.SetDamage(startDamage);
    }

    protected override void Run()
    {
        if (!isDead())
        {
            if (healthUI != null)
                healthUI.GetCanvas().GetComponent<RectTransform>().rotation = Quaternion.Euler(-90f, transform.rotation.y, 0f);
            if (target != null && canMove)
            {
                unitMove.Move(target, this);
            }
            FindClosetsTarget();
            if (attaking == false)
                timer = 0f;
        }

    }
    public void SetPool(IObjectPool<Unit> pool) => this.pool = pool;

    public void ResetMob()
    {
        if (dead)
        {
            gameObject.SetActive(true);
            SetStats();
            Debug.Log(stats.GetHealth());
            healthUI.ResetUIHealth();
            healthUI.SetHealth(stats.GetHealth());
            //myAgent.enabled = true;
            //myAgent.isStopped = false;
            timer = -1f;
            attaking = false;
            GetComponent<SphereCollider>().enabled = true;
            dead = false;
            stats.SetDamage(startDamage);

            //  stats.SetDamage(DamageController.Instance.SetDamage(startDamage));
        }

    }

    public void GiveDamage()
    {


        if (type != Mob.Mimic && type != Mob.Runner)
        {
            myAnim.SetInteger("state", 2);
            myAnim.SetLayerWeight(0, 1f);
            myAnim.SetLayerWeight(1, 0f);
        }
        if (type == Mob.Runner || type == Mob.BigPolzun || type == Mob.PolzunType2)
        {
            myAnim.SetInteger("state", 0);
            myAnim.SetLayerWeight(0, 0f);
            myAnim.SetLayerWeight(1, 1f);
        }
        Rotation(target);
        if (target.CompareTag("Player"))
            GiveDamagePlayer(Player.Instance);
        else if (target.CompareTag("Radiant"))
            GiveDamageRadiant(target.GetComponent<SLowerMan>());
    }

    public void GiveDamageRadiant(SLowerMan enemy)
    {
        if (!isDead() && attaking)
        {
            timer += Time.deltaTime;

            if (timer >= stats.GetCD())
            {
                enemy.Death();
                timer = 0f;
                attaking = false;
            }
        }
        else
            myAnim.SetInteger("state", 0);
    }

    public void GiveDamagePlayer(Player player)
    {
        if (!player.isDead() && !isDead() && attaking && !GetComponent<ControllStateUnit>().isFrost)
        {
            timer += Time.deltaTime;

            if (timer >= stats.GetCD())
            {
                player.GetDamage(stats.GetDamage());
                timer = 0f;
                attaking = false;
            }

        }
        else
            myAnim.SetInteger("state", 0);
    }


    private void Rotation(Transform target)
    {
        Vector3 dir = target.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 14 * Time.deltaTime);

    }

    private void FindClosetsTarget()
    {
        _detector.setPointInfinity();
        _detector.TryFindTheNearlestEnemy(transform.position);
        if (_detector.isDetected())
            target = _detector.GetTargetInfo();

    }

    private void SetStats()
    {
        if (type == Mob.Zombie)
            stats.SetDefaultZombie();
        else if (type == Mob.Runner)
            stats.SetDefaltRunner();
        else if (type == Mob.Alien)
            stats.SetDefaultAlien();
        else if (type == Mob.RadiactiveType2)
            stats.SetDefaultRadiactiveType2();
        else if (type == Mob.AlianType2)
            stats.SetDefaultAlianType2();
        else if (type == Mob.PolzunType2)
            stats.SetDefaultPolzunType2();
        else if (type == Mob.Samurai)
            stats.SetDefaultSamurai();
        else if (type == Mob.SamuraiType2)
            stats.SetDefaultSamuraiType2();
        else if (type == Mob.BigRadioActive)
            stats.SetDefaultBigRadioActive();
        else if (type == Mob.BigAlien)
            stats.SetDefaultBigAlian();
        else if (type == Mob.BigPolzun)
            stats.SetDefaultBigPolzun();
        else if (type == Mob.BigSamurai)
            stats.SetDefaultBigSamurai2();
        else if (type == Mob.FinalSamurai)
            stats.SetDefaultFinalSamurai();
        else if (type == Mob.BigRadioActiveType2)
            stats.SetDefaultBigRadiActiveType2();
        else if (type == Mob.BigAlianType2)
            stats.SetDefaultBigAlianType2();
        else if (type == Mob.BigPolzunType2)
            stats.SetDefaultBigPolzunType2();
        else if (type == Mob.BigSamuraiType2)
            stats.SetDefaultBigSamuraiType2();

    }

    private IEnumerator PerSecDamage(float sec, int dmg)
    {

        while (true)
        {
            Debug.Log("perSEc");
            GetDamage(dmg);
            yield return new WaitForSeconds(sec);
        }


    }

    public void GetPeriodDamage(float sec, int dmg)
    {
        StartCoroutine(PerSecDamage(sec, dmg));
    }

    public void StopPerSecDamage()
    {
        StopAllCoroutines();
    }

    public void SpawnCoins()
    {
        for (int i = 0; i < _rewardMoney; i++)
        {
            //Instantiate(coin, transform.position + Vector3.up * 6, Quaternion.Euler(0f, 0f, 90f));
            CoinSpawner.Instance.CoinSpawn(transform.position + (Vector3.up * 6f));
        }
        for (int i = 0; i < bigCoinCount; i++)
        {
            CoinSpawner.Instance.BigCoinSpawn(transform.position + (Vector3.up * 6f));
        }
        for (int i = 0; i < midleCoin; i++)
        {
            CoinSpawner.Instance.SpawnMidleCoin(transform.position + (Vector3.up * 6f));
        }
        for (int i = 0; i < boosCoinCount; i++)
        {
            CoinSpawner.Instance.SpawnBossCoin(transform.position + (Vector3.up * 6f));
        }
    }
    public void GetDamage(int dmg, int arrowdmg = 0)
    {
        if (!dead)
        {
            myAnim.SetLayerWeight(0, 0.5f);
            myAnim.SetLayerWeight(1, 1f);
            if (!attaking)
                myAnim.SetTrigger("takeDamage");
            stats.ReduceHealth(dmg);
            healthUI.GetDamageUI(dmg);
           // timer = 0f;
            ParticlePool.Instance.PlayBlood(transform.position);
            if (healthUI.GetfillAmout() <= 0)
                Death();
        }

    }



    public void Death()
    {
        unitMove.Death();
    }

    public void SetMove(bool flag) => canMove = flag;
    public bool GetAttack() { return attaking; }
    public int GetDistance() { return _distance; }
    public bool isDead() { return dead; }

    public Mob GetType() { return type; }

    public void SetAttacking(bool flag) { attaking = flag; }

    public MobStats GetStats() { return stats; }

    public ControllStateUnit GetState() { return stateUnit; }

    public void SetDeath() { dead = true; }

    public IObjectPool<Unit> getPool() { return pool; }

    public void ResetNavMesh() {GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<NavMeshAgent>().isStopped = false;
    }

    public void SetNewDamage()
    {
        Debug.Log(stats.GetDamage());
        stats.SetDamage(DamageController.Instance.SetDamage(startDamage));
    }
} 


public enum Mob {
    Zombie,
    Runner,
    Necromants,
    SmallRunner,
    Mimic,
    Alien, 
    RadiactiveType2,
    AlianType2,
    PolzunType2,
    Samurai,
    SamuraiType2,
    BigAlien,
    BigSamurai,
    BigAlianType2,
    BigSamuraiType2,
    FinalSamurai,
    BigPolzun,
    BigRadioActive,
    BigPolzunType2,
    BigRadioActiveType2
}
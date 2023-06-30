using UnityEngine;
using NTC.Global.Pool;
public class Archer : MonoBehaviour
{
    [SerializeField] private GameObject[] arrow;
    [SerializeField] private Transform arrowPos;
    [SerializeField] private MobStats stats;
    [SerializeField] private Animator myAnim;
    [SerializeField] private float radius;
    private Detector _detector;
    private float timer;
    private int indexArrow;
    private Transform target;

    private void Awake()
    {
        radius = PlayerPrefs.GetFloat("ArcherRadius", radius);
        indexArrow = PlayerPrefs.GetInt("arrowArcher", 0);
    }

    private void Start()
    {
        stats.SetDefaultArcher();
        _detector = GetComponent<Detector>();
        _detector.SetRadius(radius);
    //    stats.AddDamage(300);
    }

    private void Update()
    {
        FindClosetsTarget();
        if (target != null)
        {
            Rotation();
            GiveDamage(target.GetComponent<Unit>());
        }
        else
            myAnim.SetInteger("state", 0);
       

    }
    private void FindClosetsTarget()
    {
        _detector.setPointInfinity();
        _detector.TryFindTheNearlestEnemy(transform.position);
        if (_detector.isDetected())
            target = _detector.GetTargetInfo();

    }

    private void Rotation()
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 14 * Time.deltaTime);
        rot.x = 0f;
        rot.z = 0f;
        transform.rotation = rot;
    }

    public void GiveDamage(Unit enemy)
    {
        if (!enemy.isDead())
        {

            timer += Time.deltaTime;
            myAnim.SetInteger("state", 2);
            if (timer >= stats.GetCD())
            {
                float heightK;
                if (enemy.GetType() == Mob.Mimic)
                    heightK = 7f;
                else
                    heightK = 2f;

                GameObject prefabArrow = NightPool.Spawn(arrow[PlayerPrefs.GetInt("arrowArcher")], arrowPos.position, arrowPos.rotation);
                prefabArrow.GetComponent<Projectile>().SetDamage(stats.GetDamage());
                Rigidbody bulletBody = prefabArrow.GetComponent<Rigidbody>();
                bulletBody.velocity = ((enemy.transform.position + (Vector3.up * heightK)) - prefabArrow.transform.position).normalized * 150f;
                timer = 0f;
            }

        }
        else myAnim.SetInteger("state", 0);
    }

    public void UpgradeRadius(float value)
    {
        radius += value;
          PlayerPrefs.SetFloat("ArcherRadius", radius);
        _detector.SetRadius(radius);
    }

    public float GetRadius() { return radius; }

    public void SetNewArrow()
    {
        indexArrow = 1;
        Debug.Log(indexArrow);
        PlayerPrefs.SetInt("arrowArcher", 1);
        Debug.Log(indexArrow);
    }
}

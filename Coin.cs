
using UnityEngine;
using NTC.Global.Cache;
using UnityEngine.Pool;
public class Coin : MonoCache
{
    //ALSO NEED PHYSICS MATERIAL
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private int _distanceToCollect;
    [SerializeField] private int value = 1;
    [SerializeField] CoinType coinType;
    private Vector3 endPos;
    private Vector3 gravityPos;
    private bool collect;
    private bool trig;
    private bool canRotate;
    private float time;
    private bool gravityDown;
    private Vector3 off;
    private IObjectPool<Coin> pool;

    void Start()
    {
        
        trailRenderer.enabled = false;
        off = new Vector3(Random.Range(-4f, 4f),-1.5f, Random.Range(-4f, 4f));
        endPos = transform.position + off;
    }

    public void SetPool(IObjectPool<Coin> pool) => this.pool = pool;


    protected override void Run()
    {

        if (!trig)
        {
            time += Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, endPos, time);
            if (transform.position == endPos)
            {
                trig = true;
                canRotate = true;
                sphereCollider.enabled = true;
                gravityPos = transform.position;
                time = 0f;
            }
        }
        else if (canRotate)
        {
            #region gravity
            //if (!gravityDown)
            //{
            //    time += Time.deltaTime;
            //    transform.position = Vector3.MoveTowards(transform.position, gravityPos + Vector3.up, time * 0.005f);
            //    if (transform.position == gravityPos + Vector3.up)
            //    {
            //        gravityDown = true;
            //        time = 0f;
            //    }

            //}
            //if (gravityDown)
            //{
            //    time += Time.deltaTime;
            //    transform.position = Vector3.MoveTowards(transform.position, gravityPos - Vector3.up, time * 0.005f);
            //    if(transform.position == gravityPos - Vector3.up)
            //    {
            //        gravityDown = false;
            //        time = 0f;
            //    }
            //}
            #endregion gravity
            transform.Rotate(Vector3.right * 3f);
        }

        if (collect)
        {
            trailRenderer.enabled = true;
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, Player.Instance.transform.position, time);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, time);
            if (transform.localScale == Vector3.zero)
            {
                Bank.Instance.AddCoins(value);
                //   Destroy(gameObject);
                if (coinType == CoinType.BossCoin)
                    Destroy(gameObject);
                else
                    pool.Release(this);
            }
               
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && trig)
            collect = true;
    }

    
    public void ResetCoin()
    {
        gameObject.SetActive(true);
        if(coinType == CoinType.Coin)
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else if(coinType == CoinType.BigCoin)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if(coinType == CoinType.MidleCoin)
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        else if(coinType == CoinType.BossCoin)
            transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        trig = false;
        canRotate = false;
        collect = false;
        sphereCollider.enabled = false;
        trailRenderer.enabled = false;
        time = 0f;
       
    }
    
    public void SetEndPos()
    {
        off = new Vector3(Random.Range(-4f, 4f), -1.5f, Random.Range(-4f, 4f));
        endPos = transform.position + off;
    }

   public void SetRadius(int value)
    {
        _distanceToCollect = value;
        sphereCollider.radius = _distanceToCollect;
    }

    public void SetCollect() => collect = true;


}

public enum CoinType {Coin,BigCoin , MidleCoin ,BossCoin}
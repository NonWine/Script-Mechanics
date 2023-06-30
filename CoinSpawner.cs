using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NTC.Global.Pool;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    public static CoinSpawner Instance { get; private set; }
    [SerializeField] private Coin coin;
    [SerializeField] private Coin bigCoin;
    [SerializeField] private Coin midleCoin;
    [SerializeField] private Coin BossCoin;
    [SerializeField] private int radiusCollect;
    [SerializeField] private int radiusCollectBigCoin;
    private ObjectPool<Coin> coinPool;
    private ObjectPool<Coin> bigCoinPool;
    private ObjectPool<Coin> midleCoinPool;

    private void Awake()
    {
        Instance = this;
       radiusCollect =  PlayerPrefs.GetInt("distanceCollect", radiusCollect);
        radiusCollectBigCoin = PlayerPrefs.GetInt("distanceToColletcBigCoin", radiusCollectBigCoin);
    }

    private void Start()
    {
        coinPool = new ObjectPool<Coin>(CreateFuncPoolCoin, GetFuncRadio, ReasleFunc);
        bigCoinPool = new ObjectPool<Coin>(CreateFuncPoolBigCoin, GetFuncRadio, ReasleFunc);
        midleCoinPool = new ObjectPool<Coin>(CreateFuncPoolMidleCoin, GetFuncRadio, ReasleFunc);
    }

    private Coin CreateFuncPoolCoin()
    {
        var mob = Instantiate(coin);
        mob.GetComponent<Coin>().SetPool(coinPool);
        return mob.GetComponent<Coin>();
    }

    private Coin CreateFuncPoolMidleCoin()
    {
        var mob = Instantiate(midleCoin);
        mob.GetComponent<Coin>().SetPool(midleCoinPool);
        return mob.GetComponent<Coin>();
    }

    private Coin CreateFuncPoolBigCoin()
    {
        var mob = Instantiate(bigCoin);
        mob.GetComponent<Coin>().SetPool(bigCoinPool);
        return mob.GetComponent<Coin>();
    }

    private void GetFuncRadio(Coin obj)
    {
        obj.ResetCoin();
    }

    private void ReasleFunc(Coin obj)
    {
      
        obj.gameObject.SetActive(false);
    }


    public void CoinSpawn(Vector3 pos)
    {
        var Coin = coinPool.Get();
        Coin.transform.position = pos;
        Coin.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        Coin.SetEndPos();
        Coin.SetRadius(radiusCollect);
    }

    public void SpawnMidleCoin(Vector3 pos)
    {
        var Coin = midleCoinPool.Get();
        Coin.transform.position = pos;
        Coin.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        Coin.SetEndPos();
        Coin.SetRadius(radiusCollect);
    }

    public void BigCoinSpawn(Vector3 pos)
    {
        var Coin = bigCoinPool.Get();
        Coin.transform.position = pos;
        Coin.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        Coin.SetEndPos();
        Coin.SetRadius(radiusCollectBigCoin);
    }

    public void SpawnBossCoin(Vector3 pos)
    {
        Instantiate(BossCoin, pos, Quaternion.Euler(0f,0f,90f));
    }

    public void IncreaseDistance(int value)
    {
        radiusCollect += value;
        radiusCollectBigCoin += value / 2;
        PlayerPrefs.SetInt("distanceCollect", radiusCollect);
        PlayerPrefs.SetInt("distanceToColletcBigCoin", radiusCollectBigCoin);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }
    [SerializeField] private Unit radioActive;
    [SerializeField] private Unit alian;
    [SerializeField] private Unit polzun;
    [SerializeField] private Unit radioActiveType2;
    [SerializeField] private Unit alianType2;
    [SerializeField] private Unit polzunType2;
    [SerializeField] private Unit samurai;
    [SerializeField] private Unit samuraiType2;
    private ObjectPool<Unit> poolRadoiActive;
    private ObjectPool<Unit> poolAlian;
    private ObjectPool<Unit> poolpolzun;
    private ObjectPool<Unit> poolRadioActiveType2;
    private ObjectPool<Unit> poolAlian2;
    private ObjectPool<Unit> poolPolzunType2;
    private ObjectPool<Unit> poolSamurai;
    private ObjectPool<Unit> poolSamuraiType2;
    private List<GameObject> radioActiveList = new List<GameObject>();
    private List<GameObject> alianList = new List<GameObject>();
    private List<GameObject> polzunList = new List<GameObject>();
    private List<GameObject> samuaraiList = new List<GameObject>();
    private List<GameObject> radioactive2List = new List<GameObject>();
    private List<GameObject> alian2List = new List<GameObject>();
    private List<GameObject> polzun2List = new List<GameObject>();
    private List<GameObject> samurai2List = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        poolRadioActiveType2 = new ObjectPool<Unit>(CreateFuncPoolRadioActiveType2, GetFuncPool, ReasleFuncPool,DestroyPool);
        poolpolzun = new ObjectPool<Unit>(CreateFuncPoolPolzun, GetFuncPool, ReasleFuncPool,DestroyPool);
        poolRadoiActive = new ObjectPool<Unit>(CreateFuncPoolRadioActive, GetFuncPool, ReasleFuncPool,DestroyPool);
        poolAlian = new ObjectPool<Unit>(CreateFuncALian, GetFuncPool, ReasleFuncPool,DestroyPool);
        poolAlian2 = new ObjectPool<Unit>(CreateFuncPoolAlianType2, GetFuncPool, ReasleFuncPool,DestroyPool);
        poolPolzunType2 = new ObjectPool<Unit>(CreateFuncPoolPolzunType2, GetFuncPool, ReasleFuncPool, DestroyPool);
        poolSamurai = new ObjectPool<Unit>(CreateFuncPoolSamurai, GetFuncPool, ReasleFuncPool, DestroyPool);
        poolSamuraiType2 = new ObjectPool<Unit>(CreateFuncPoolSamuraiType2, GetFuncPool, ReasleFuncPool, DestroyPool);
    }

    public Unit SpawnSamurai()
    {
        return poolSamurai.Get();
    }

    public Unit SpawnSamuraiType2()
    {
        return poolSamuraiType2.Get();
    }

    public Unit SpawnPolzunType2()
    {
        return poolPolzunType2.Get();
    }

    public Unit SpawnAlian2()
    {
        return poolAlian2.Get();
    }

    public Unit SpawnRadioActive()
    {
       return poolRadoiActive.Get();
    }

    public Unit SpawnAlian()
    {
        return poolAlian.Get();
    }
    public Unit SpawnPolzun()
    {
        return poolpolzun.Get();
    }

    public Unit SpawnRadioActiveType2()
    {
        return poolRadioActiveType2.Get();
    }

    public void ClearPoolRadioActive()
    {
        poolRadoiActive.Dispose();
       
    }
    public void ClearPoolALian() => poolAlian.Dispose();

    public void ClearPoolPolzun() => poolpolzun.Dispose();

    private Unit CreateFuncPoolSamurai()
    {
        var mob = Instantiate(samurai);
        mob.GetComponent<Unit>().SetPool(poolSamurai);
        samuaraiList.Add(mob.gameObject);
        return mob.GetComponent<Unit>();
    }

    private Unit CreateFuncPoolSamuraiType2()
    {
        var mob = Instantiate(samuraiType2);
        mob.GetComponent<Unit>().SetPool(poolSamuraiType2);
        samurai2List.Add(mob.gameObject);
        return mob.GetComponent<Unit>();
    }

    private Unit CreateFuncPoolAlianType2()
    {
        var mob = Instantiate(alianType2);
        mob.GetComponent<Unit>().SetPool(poolAlian2);
        alian2List.Add(mob.gameObject);
        return mob.GetComponent<Unit>();
    }

    private Unit CreateFuncPoolPolzunType2()
    {
        var mob = Instantiate(polzunType2);
        mob.GetComponent<Unit>().SetPool(poolPolzunType2);
        polzun2List.Add(mob.gameObject);
        return mob.GetComponent<Unit>();
    }

    private Unit CreateFuncPoolRadioActiveType2()
    {
        var mob = Instantiate(radioActiveType2);
        mob.GetComponent<Unit>().SetPool(poolRadioActiveType2);
        radioactive2List.Add(mob.gameObject);
        return mob.GetComponent<Unit>();
    }

    private Unit CreateFuncPoolPolzun()
    {
        var mob = Instantiate(polzun);
        mob.GetComponent<Unit>().SetPool(poolpolzun);
        polzunList.Add(mob.gameObject);
        return mob.GetComponent<Unit>();
    }

    private Unit CreateFuncPoolRadioActive()
    {
        var mob = Instantiate(radioActive);
        mob.GetComponent<Unit>().SetPool(poolRadoiActive);
        radioActiveList.Add(mob.gameObject);
        return mob.GetComponent<Unit>();
    }

    private Unit CreateFuncALian()
    {
        var mob = Instantiate(alian);
        mob.GetComponent<Unit>().SetPool(poolAlian);
        alianList.Add(mob.gameObject);
        return mob.GetComponent<Unit>();
    }

    private void GetFuncPool(Unit obj)
    {
        obj.ResetMob();
    }

    private void ReasleFuncPool(Unit obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void DestroyPool(Unit obj) 
    {
        Destroy(obj);
    }

    public void ClearRadiActive()
    {
        for (int i = 0; i < radioActiveList.Count; i++)
        {
            GameObject obj = radioActiveList[i];
            Destroy(obj);
        }
        radioActiveList.Clear();
        poolRadoiActive.Dispose();
    }

    public void ClearAlian()
    {
        for (int i = 0; i < alianList.Count; i++)
        {
            GameObject obj = alianList[i];
            Destroy(obj);
        }
        alianList.Clear();
        poolAlian.Dispose();
    }

    public void ClearPolzun()
    {
        for (int i = 0; i < polzunList.Count; i++)
        {
            GameObject obj = polzunList[i];
            Destroy(obj);
        }
        polzunList.Clear();
        poolpolzun.Dispose();
    }

    public void ClearSamurai()
    {
        for (int i = 0; i < samuaraiList.Count; i++)
        {
            GameObject obj = samuaraiList[i];
            Destroy(obj);
        }
        samuaraiList.Clear();
        poolSamurai.Dispose();
    }

    public void ClearRadioActiveType2()
    {
        for (int i = 0; i < radioactive2List.Count; i++)
        {
            GameObject obj = radioactive2List[i];
            Destroy(obj);
        }
        radioactive2List.Clear();
        poolRadioActiveType2.Dispose();
    }

    public void ClearAlian2()
    {
        for (int i = 0; i < alian2List.Count; i++)
        {
            GameObject obj = alian2List[i];
            Destroy(obj);
        }
        alian2List.Clear();
        poolAlian2.Dispose();
    }

    public void ClearPolzun2()
    {
        for (int i = 0; i < polzun2List.Count; i++)
        {
            GameObject obj = polzun2List[i];
            Destroy(obj);
        }
        polzun2List.Clear();
        poolPolzunType2.Dispose();
    }

    public void ChangeDamage()
    {
        foreach (var item in alian2List)
        {
            item.GetComponent<Unit>().SetNewDamage();
            
        }

        foreach (var item in alianList)
        {
            item.GetComponent<Unit>().SetNewDamage();
        }

        foreach (var item in radioActiveList)
        {
            item.GetComponent<Unit>().SetNewDamage();
            Debug.Log("word");
        }
        foreach (var item in radioactive2List)
        {
            item.GetComponent<Unit>().SetNewDamage();
        }

        foreach (var item in polzunList)
        {
            item.GetComponent<Unit>().SetNewDamage();
        }

        foreach (var item in polzun2List)
        {
            item.GetComponent<Unit>().SetNewDamage();
        }

        foreach (var item in samuaraiList)
        {
            item.GetComponent<Unit>().SetNewDamage();
        }
        foreach (var item in samurai2List)
        {
            item.GetComponent<Unit>().SetNewDamage();
        }

    }

}

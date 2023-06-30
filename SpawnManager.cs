using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private LineProgress lineProgress;
    [SerializeField] private float timeDelay;
    [SerializeField] private float reduceDelay;
    [SerializeField] private float timeLimitDelay;
    [SerializeField] private int setTestWave;
    [SerializeField] private EndlessWaves endlessWaves;
    [SerializeField] private GameObject boss;
    private int waveId;
    private int generalCounts;
    private int spawnIndex;
    private int indexAfterBoss;
    private void Awake()
    {
        Instance = this;
        timeDelay = PlayerPrefs.GetFloat("timeDelay", timeDelay);
         waveId = PlayerPrefs.GetInt("waveId", 0); // start LEVEL\Get Current LEVEL
        indexAfterBoss = PlayerPrefs.GetInt("indexAfterBoss", 0);

    }
    

    public void Spawn()
    {
        StartCoroutine(SpawnCor());

    }

    private IEnumerator SpawnCor()
    {
        int j = 0;
        Debug.Log("spawn");
        for (int i = 0; i <= waves[waveId].Count[j]; i++)
        {

            if (i == waves[waveId].Count[j])
            {
                i = 0;
                j++;
                if (j == waves[waveId].Count.Length)
                    break;
            }
            generalCounts++;
        }
        lineProgress.SetMobs(generalCounts);

        j = 0;
        for (int i = 0; i <= waves[waveId].Count[j]; i++)
        {

            if (i == waves[waveId].Count[j])
            {
                i = 0;
                j++;
                if (j == waves[waveId].Count.Length)
                    break;
                yield return new WaitForSeconds(timeDelay);
            }
          Instantiate(waves[waveId].Mob[j], spawnPoints[Random.Range(0,spawnPoints.Length)].position, Quaternion.identity);
          //Unit unit =   Pool.Instance.GetDino(waves[waveId].Mob[j].GetComponent<Unit>(), waves[waveId].Mob[j].GetComponent<Unit>().GetType());
          //  unit.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
          //  unit.transform.rotation = Quaternion.identity;
          //  unit.gameObject.SetActive(true);
        }
    }

    public void AddDeathEnemy()
    {
        generalCounts--;
        lineProgress.UpdateLineProgress();
        Debug.Log(generalCounts);
        if (generalCounts == 0)
        {
            GameManager.Instance.GameWin();
        }
          
    }

    public void AddlevelWave()
    {
        //saveLEVEL
        waveId++;
        PlayerPrefs.SetInt("waveId", waveId);
        if (waveId == waves.Length || waveId == 10)
        {
            waveId = 10;
            int index = Random.Range(0, waves[waveId].Mob.Length);
            GameObject unit = waves[waveId].Mob[index];
           Unit currentUnit = unit.GetComponent<Unit>();
           if(currentUnit.GetType() == Mob.Runner)
            {
                waves[waveId].Count[index] += 1;
            }
           else if(currentUnit.GetType() == Mob.Zombie)
            {
                waves[waveId].Count[index] += 3;
            }
            indexAfterBoss++;
            PlayerPrefs.SetInt("indexAfterBoss", indexAfterBoss);
            if(indexAfterBoss == 5)
            {
                indexAfterBoss = 0;
                waves[waveId].Count[waves[waveId].Count.Length - 1]++;
                PlayerPrefs.SetInt("indexAfterBoss", 0);
            }
          ///  PlayerPrefs.DeleteAll();
          ///  SceneManager.LoadScene(0);
          
        }
        timeDelay -= reduceDelay;
        if (timeDelay < timeLimitDelay)
            timeDelay = timeLimitDelay;
        PlayerPrefs.SetFloat("timeDelay", timeDelay);
    }

    [ContextMenu("SetTestWave")]
    public void SetTestLevel()
    {
        PlayerPrefs.SetInt("waveId", setTestWave);
    }
}

public class BaseWave
{

}

[System.Serializable]
public class Wave : BaseWave
{
    public GameObject[] Mob;
    public int[] Count;
}
[System.Serializable]
public class EndlessWaves : BaseWave
{
    public GameObject[] Mob;
    public int[] Count;
    
    public void SpawnMobs()
    {
       
    }
   
}
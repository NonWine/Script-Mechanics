using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image hpfill;
    [SerializeField] private Image WhiteFill;
    private float _speedLerp;
    private float health;
    private float percent;
    private float forpercent;
    private Coroutine _canvasCd;
    private float playerP;

    private void Awake()
    {
        if (gameObject.CompareTag("Player"))
        {
            playerP = PlayerPrefs.GetFloat("playerHpUI",1f);
            
            //hpfill.fillAmount = playerP;
            //WhiteFill.fillAmount = playerP;
        }
    }

    private void Start()
    {
       
        _speedLerp = 1f;
        if (!gameObject.CompareTag("Player"))
            canvas.SetActive(false);
       
    }


    public void SetHealth(float value)
    {
        health = value;
        forpercent = health;
    }

    public void GetDamageUI(float count)
    {
        if (!gameObject.CompareTag("Player"))
        {
            if (_canvasCd != null)
                StopCoroutine(_canvasCd);
            _canvasCd = StartCoroutine(CanvasCD());
        }
        StartCoroutine(ReducHp(count));
    }

    public void TurnOffUiHP()
    {
        canvas.SetActive(false);
        enabled = false;
    } 

    private IEnumerator ReducHp(float count)
    {
        float speed = 0;
        percent = ((forpercent - (health - count)) / forpercent);
        hpfill.fillAmount -= percent;
        while (WhiteFill.fillAmount != hpfill.fillAmount)
        {
            speed += Time.deltaTime;
            WhiteFill.fillAmount = Mathf.Lerp(WhiteFill.fillAmount, hpfill.fillAmount, speed * _speedLerp);
            if (WhiteFill.fillAmount <= 0)
            {
                TurnOffUiHP();
                break;
            }
            yield return null;
        }
        PlayerPrefs.SetFloat("playerHpUI", hpfill.fillAmount);
    }

    public float GetfillAmout() { return hpfill.fillAmount; }

    public void ResetUIHealth()
    {
        hpfill.fillAmount = 1f;
        WhiteFill.fillAmount = 1f;
        gameObject.SetActive(true);
        enabled = true;
    }

    private IEnumerator CanvasCD()
    {
        
        canvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        canvas.SetActive(false);
    }

    public GameObject GetCanvas() { return canvas; }

    public int Health() { return (int)health; }
}

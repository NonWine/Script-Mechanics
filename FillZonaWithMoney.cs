using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ExapndArea : MonoBehaviour
{
    [SerializeField] private Image _FilledImage;
    [SerializeField] private float cost;
    [SerializeField] private TextMeshProUGUI _costTexts;
    [SerializeField] private float percentModifier;
    private Coroutine cor;
    private float percent;
    private bool canBuy;
    private float forpercent;
    private bool trig;

    private void Start()
    {

        Debug.Log(Mathf.RoundToInt(percentModifier));
        _costTexts.text = cost.ToString();
        forpercent = cost;
        float tcost = cost;
        while (tcost > 9)
        {
            tcost = tcost / 10;
            percentModifier *= 10;
        }
        percentModifier = cost * (percentModifier / cost);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cor = StartCoroutine(FillZona());
            canBuy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canBuy = false;
            StopCoroutine(cor);
        }
    }

    private IEnumerator FillZona()
    {
        float Tcost = cost;
        float bankCost = cost;
        _FilledImage.fillAmount = percent;
        while (Tcost > 0)
        {

            if (Bank.Instance.CoinsCount > percentModifier)
            {
                percent = ((forpercent - (Tcost - percentModifier)) / forpercent);
                Tcost -= percentModifier;
                cost -= percentModifier;
                if (Tcost < 0)
                    Tcost = 0;
                _costTexts.SetText(Mathf.RoundToInt(Tcost).ToString());
                _FilledImage.fillAmount = percent;
                if (bankCost > 0)
                {
                    Bank.Instance.ReduceCoins(Mathf.RoundToInt(percentModifier));
                    bankCost--;
                }
            }
            else
                StopCoroutine(cor);
            yield return null;
        }

        if (_FilledImage.fillAmount == 1f && !trig)
        {
            trig = true;
        }
    }
}
using UnityEngine;
using TMPro;

public class TimerText : MonoBehaviour
{
    [SerializeField] private float timerLeft;
    [SerializeField] TMP_Text _timerText;
    private bool timerOn;

    private void Start()
    {
        timerOn = true;
    }

    private void Update()
    {
        if (timerOn)
        {
            if(timerLeft > 0f)
            {
                timerLeft -= Time.deltaTime;
                UpdateTimer(timerLeft);
            }
            else
            {
                timerLeft = 0f;
                timerOn = false;
            }
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1f;
        float minutes = Mathf.FloorToInt(currentTime / 60f);
        float seconds = Mathf.FloorToInt(currentTime % 60f);
        _timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public event System.Action Faild;

    public Image Timer;
    public float StartingTime;
    public float Minutes, Seconds;
    public Text MinutesText, SecondsText;
    
    private float timeElapsed, totalTime;

    void Start()
    {
        StartingTime *= 60;
        totalTime = StartingTime;
        Timer.fillAmount = 1;
        StartCoroutine(ClocksTicking());
    }

    IEnumerator ClocksTicking()
    {
        do
        {
            timeElapsed += Time.deltaTime;
            Timer.fillAmount = Mathf.Clamp01((totalTime - timeElapsed) / totalTime);
            Minutes = Mathf.Clamp(Mathf.FloorToInt((totalTime - timeElapsed) / 60f),0,Mathf.Infinity);
            Seconds = Mathf.Clamp(Mathf.FloorToInt((totalTime - timeElapsed) % 60f), 0, Mathf.Infinity);

            MinutesText.text = Minutes.ToString();
            SecondsText.text = Seconds.ToString();

            if(Minutes < 10)
               MinutesText.text = "0" + Minutes.ToString();
            if (Seconds < 10)
                SecondsText.text = "0" + Seconds.ToString();
                
            yield return null;
        }
        while (Timer.fillAmount > 0);

        if (Faild != null)
            Faild();
    }
}

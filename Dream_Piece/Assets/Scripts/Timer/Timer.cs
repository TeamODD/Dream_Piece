//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 20f;
    float timeLeft;
    public GameObject TimesUpText;


    void Start()
    {
        TimesUpText.SetActive(false);
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    void Update()
    {   
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            TimesUpText.SetActive(true);
            Time.timeScale = 0;
            // Add any additional logic for when the timer runs out
        }





    }

}

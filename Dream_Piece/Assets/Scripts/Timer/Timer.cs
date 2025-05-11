//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 20f;
    float timeLeft;
    public GameObject TimesUpText;
    public GameObject FailPanel;


    void Start()
    {
        TimesUpText.SetActive(false);
        FailPanel.SetActive(false);
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
            FailPanel.SetActive(true);
            // Add any additional logic for when the timer runs out
        }





    }

}

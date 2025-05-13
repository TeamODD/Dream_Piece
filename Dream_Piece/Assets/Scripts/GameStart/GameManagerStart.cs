using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject HowPanel;

    public void Start()
    {
        HowPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HowPanel.SetActive(false);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void OpenHowTo()
    {
        HowPanel.SetActive(true);
    }
}
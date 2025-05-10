using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerSub : MonoBehaviour
{
    public GameObject pausePanel;         // �Ͻ����� UI �г�
    private bool isPaused = false;        // �Ͻ����� ���� üũ

    void Start()
    {
        pausePanel.SetActive(false);      // ������ �� �г� �����
        Time.timeScale = 1f;             // ���� �ð� ����ȭ
    }

    void Update()
    {
        // ESC Ű �Է� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    void PauseGame()
    {
        pausePanel.SetActive(true);       // �Ͻ����� �޴� ǥ��
        Time.timeScale = 0f;             // ���� �ð� ����
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);      // �Ͻ����� �޴� �����
        Time.timeScale = 1f;             // ���� �ð� �簳
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;             // ���� �ð� ����ȭ
        SceneManager.LoadScene("MainMenu"); // ���� �޴� ������ �̵�
    }

    public void QuitGame()
    {
        Application.Quit();              // ���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ��� ���� ����
#endif
    }
}

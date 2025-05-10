using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerSub : MonoBehaviour
{
    public GameObject pausePanel;         // 일시정지 UI 패널
    private bool isPaused = false;        // 일시정지 상태 체크

    void Start()
    {
        pausePanel.SetActive(false);      // 시작할 때 패널 숨기기
        Time.timeScale = 1f;             // 게임 시간 정상화
    }

    void Update()
    {
        // ESC 키 입력 감지
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
        pausePanel.SetActive(true);       // 일시정지 메뉴 표시
        Time.timeScale = 0f;             // 게임 시간 정지
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);      // 일시정지 메뉴 숨기기
        Time.timeScale = 1f;             // 게임 시간 재개
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;             // 게임 시간 정상화
        SceneManager.LoadScene("MainMenu"); // 메인 메뉴 씬으로 이동
    }

    public void QuitGame()
    {
        Application.Quit();              // 게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // 에디터에서 실행 중지
#endif
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManagerSub : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;
    public Timer timer;

    [Header("�����̴�")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Header("Audio Mixer")]
    public AudioMixer mainMixer;

    public bool isGrounded = true; // �ٴڿ� ���� ���� ���� ���

    private bool isRunningSoundPlaying = false;

    void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        float bgmVol = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        bgmSlider.value = bgmVol;
        sfxSlider.value = sfxVol;

        SetBGMVolume(bgmVol);
        SetSFXVolume(sfxVol);

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Stage1Scene")
        {
            SoundManager.Instance.PlayBGM("Stage1BGM");
        }
        else if (currentSceneName == "Stage2Scene")
        {
            SoundManager.Instance.PlayBGM("Stage2BGM");
        }
        else if (currentSceneName == "Stage3Scene")
        {
            SoundManager.Instance.PlayBGM("Stage3BGM");
        }
    }

    void Update()
    {
        // ESC ������ ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.Instance.PlaySFX("escClip");

            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }


        // �� A, D �ȱ� ���� (��� ���)
        if (isGrounded && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
        {
            if (!isRunningSoundPlaying)
            {
                //SoundManager.Instance.PlayRunLoop();
                isRunningSoundPlaying = true;
            }
        }

        // �� Ű���� �� ���� �� ����
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            SoundManager.Instance.StopRunLoop();
            isRunningSoundPlaying = false;
        }
    }

    void PauseGame()
    {
        if (!timer.isFail)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        SoundManager.Instance.PlaySFX("buttonClip");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void SetBGMVolume(float value)
    {
        if (value <= 0.0001f)
        {
            mainMixer.SetFloat("BGM", -80f); // ���� ����
        }
        else
        {
            mainMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
        }

        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        if (value <= 0.0001f)
        {
            mainMixer.SetFloat("SFX", -80f); // ���� ����
        }
        else
        {
            mainMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        }

        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void Again()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Stage1Scene")
        {
            SceneManager.LoadScene("Stage1Scene");
        }
        else if (currentSceneName == "Stage2Scene")
        {
            SceneManager.LoadScene("Stage2Scene");
        }
        else if (currentSceneName == "Stage3Scene")
        {
            SceneManager.LoadScene("Stage3Scene");
        }
    }
}

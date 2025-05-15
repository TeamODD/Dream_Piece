using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManagerSub : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    [Header("슬라이더")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Header("Audio Mixer")]
    public AudioMixer mainMixer;

    void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        // 슬라이더 초기화
        float bgmVol = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        bgmSlider.value = bgmVol;
        sfxSlider.value = sfxVol;

        SetBGMVolume(bgmVol);
        SetSFXVolume(sfxVol);

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == "Stage1Scene")
        {
            SoundManager.Instance.PlayBGM("Stage1BGM");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SoundManager.instance.PlaySFX(SoundManager.instance.escClip);

            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        //SoundManager.instance.PlaySFX(SoundManager.instance.buttonClip);    
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
        mainMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        mainMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}
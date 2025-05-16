using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject HowPanel;

    public void Start()
    {
        SoundManager.Instance.PlayBGM("DefaultBGM");
        HowPanel.SetActive(false);

        float bgmVol = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        float masterVol = PlayerPrefs.GetFloat("MasterVolume", 1f);

        SetMixerVolume("BGM", bgmVol);
        SetMixerVolume("SFX", sfxVol);
        SetMixerVolume("Master", masterVol);
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
        SceneManager.LoadScene("StartAniScene");
    }

    public void OpenHowTo()
    {
        HowPanel.SetActive(true);
    }

    void SetMixerVolume(string parameter, float value)
    {
        float volume = Mathf.Clamp(value, 0.0001f, 1f);
        float db = Mathf.Log10(volume) * 20f;
        SoundManager.Instance.audioMixer.SetFloat(parameter, db);
    }

    public void End()
    {
        Application.Quit();
    }
}
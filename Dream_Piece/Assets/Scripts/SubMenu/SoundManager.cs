using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip jumpClip;
    public AudioClip buttonClip;
    public AudioClip escClip;
    public AudioClip runClip;

    [Header("BGM")]
    public AudioSource bgmSource;
    public List<AudioClip> bgmList = new();  // 0~5 인덱스별 BGM 저장

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlaySceneBGM(SceneManager.GetActiveScene().name);  // ✅ 씬 이름 기준 BGM 재생
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneBGM(scene.name);
    }

    void PlaySceneBGM(string sceneName)
    {
        bgmSource.Stop();

        int index = sceneName switch
        {
            "MainMenu" => 0,
            "StageSelect" => 1,
            "Stage1Scene" => 2,
            "Stage2Scene" => 3,
            "Stage3Scene" => 4,
            "EndingScene" => 5,
            _ => -1,
        };

        if (index >= 0 && index < bgmList.Count && bgmList[index] != null)
        {
            bgmSource.clip = bgmList[index];
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}

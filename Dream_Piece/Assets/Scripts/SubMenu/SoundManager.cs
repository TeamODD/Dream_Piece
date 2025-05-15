using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [SerializeField]
    private List<AudioClip> bgmClips;

    private Dictionary<string, AudioClip> bgmDict = new();

    [SerializeField]
    private List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> sfxDict = new();

    public static SoundManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (var clip in sfxClips)
        {
            sfxDict[clip.name] = clip;
        }

        foreach (var clip in bgmClips)
        {
            Debug.Log($"[BGM Init] Clip name = {clip.name}");
            bgmDict[clip.name] = clip;
        }
    }

    void Start()
    {

    }

    public void PlaySFX(string name)
    {
        if (sfxDict.TryGetValue(name, out var clip))
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayBGM(string name, bool loop = true)
    {
        Debug.Log($"[PlayBGM] 호출됨: {name}");

        if (bgmDict == null || bgmDict.Count == 0)
        {
            Debug.LogWarning("[PlayBGM] bgmDict가 비어 있음");
            return;
        }

        if (bgmDict.TryGetValue(name, out var clip))
        {
            Debug.Log($"[PlayBGM] 클립 '{name}' 재생 시작");
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($"[PlayBGM] 클립 '{name}'을 bgmDict에서 찾지 못함");
        }
    }

    // ✅ 추가: 걷기 효과음 루프 전용 함수
    public void PlayRunLoop()
    {
        if (sfxDict.TryGetValue("runClip", out var clip))
        {
            sfxSource.clip = clip;
            sfxSource.loop = true;
            sfxSource.Play();
        }
    }

    public void StopRunLoop()
    {
        if (sfxSource.clip != null && sfxSource.clip.name == "runClip")
        {
            sfxSource.Stop();
            sfxSource.loop = false;
            sfxSource.clip = null;
        }
    }
}

using System.Collections;
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

    // SMH bug fix
    [SerializeField] private int sfxSourcePoolSize = 10;
    private List<AudioSource> sfxSources = new();
    private int sfxSourceIndex = 0;
    /*
    private Dictionary<string, float> sfxVolumeDict = new()
{
    { "Die", 10.0f },
    { "Item", 10.0f }
};
    */
    // SMH bug fix

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

        // SMH bug fix
        for (int i = 0; i < sfxSourcePoolSize; i++)
        {
            var newSource = gameObject.AddComponent<AudioSource>();
            newSource.outputAudioMixerGroup = sfxSource.outputAudioMixerGroup;
            sfxSources.Add(newSource);
        }
        // SMH bug fix
    }

    void Start()
    {

    }

    public void PlaySFX(string name)
    {
        if (sfxDict.TryGetValue(name, out var clip))
        {
            // SMH bug fix
            /*
            float volume = 1.0f;
            if (sfxVolumeDict.TryGetValue(name, out var customVolume))
            {
                volume = customVolume;
            }
            */
            //sfxSource.PlayOneShot(clip);
            var source = sfxSources[sfxSourceIndex];
            source.PlayOneShot(clip);
            sfxSourceIndex = (sfxSourceIndex + 1) % sfxSources.Count;
            // SMH bug fix
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
    public void PlayRunLoop(bool isGrounded)    // SMH bug fix --> previous : public void PlayRunLoop()
    {
        if (!isGrounded) return;    // SMH bug fix

        if (sfxDict.TryGetValue("runClip", out var clip))
        {
            if (sfxSource.clip == clip && sfxSource.isPlaying) return;  // SMH bug fix

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

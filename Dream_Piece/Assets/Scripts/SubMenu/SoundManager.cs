using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource sfxSource;  // 효과음용 AudioSource
    public AudioClip jumpClip;
    public AudioClip buttonClip;
    public AudioClip escClip;
    public AudioClip runClip;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}

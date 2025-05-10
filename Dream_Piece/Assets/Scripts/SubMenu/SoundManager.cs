using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource sfxSource;  // ȿ������ AudioSource
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

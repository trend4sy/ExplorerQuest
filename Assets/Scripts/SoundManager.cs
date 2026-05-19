using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip starSound;
    public AudioClip clickSound;
    public AudioClip bgMusic;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }

        if (bgMusic != null)
        {
            audioSource.clip = bgMusic;
            audioSource.loop = true;
            audioSource.volume = 0.4f;
            audioSource.Play();
        }
    }

    public void PlayCorrect() => Play(correctSound);
    public void PlayWrong()   => Play(wrongSound);
    public void PlayStar()    => Play(starSound);
    public void PlayClick()   => Play(clickSound);

    void Play(AudioClip clip)
    {
        if (clip != null) audioSource.PlayOneShot(clip);
    }
}

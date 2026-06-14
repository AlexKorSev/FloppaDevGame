using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip backgroundM;
    public AudioClip jump;
    public AudioClip coin;
    public AudioClip attack;
    public AudioClip damageTaken;
    public AudioClip doorMove;
    public AudioClip healinTaken;
    public AudioClip spikes;
    public AudioClip projectileImpact;
    public AudioClip landing;

    public void Start()
    {
        musicSource.clip = backgroundM;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.pitch = Random.Range(0.9f, 1.1f); // Тот самый разный звук
        SFXSource.PlayOneShot(clip);
        //SFXSource.PlayOneShot(clip);
    }
}

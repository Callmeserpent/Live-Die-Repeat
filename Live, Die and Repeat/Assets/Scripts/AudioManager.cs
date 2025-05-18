using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------- Audio Source -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------------- Audio Clip -----------")]
    public AudioClip background;
    public AudioClip heal;
    public AudioClip slash;
    public AudioClip die;
    public AudioClip walk;
    private static AudioManager instance;

    private void start(){
        musicSource.clip = background;
        musicSource.Play();
    }
    
    public void PlaySFX(AudioClip clip, float pitch = 1.0f)
    {   
        SFXSource.pitch = pitch;  // Adjust playback speed
        SFXSource.PlayOneShot(clip);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps AudioManager alive across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicate instances
        }
    }
}

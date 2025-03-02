using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource backgroundMusicSource;
    public AudioSource soundEffectSource;

    private void Awake()
    {
        // Asegurar que solo haya un SoundManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistencia entre escenas
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Reproducir música de fondo (loop opcional)
    public void PlayBackgroundMusic(AudioClip musicClip, bool loop = true)
    {
        if (backgroundMusicSource.clip != musicClip)
        {
            backgroundMusicSource.clip = musicClip;
            backgroundMusicSource.loop = loop;
            backgroundMusicSource.Play();
        }
    }

    // Detener la música de fondo
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    // Reproducir un efecto de sonido
    public void PlaySoundEffect(AudioClip soundClip)
    {
        soundEffectSource.PlayOneShot(soundClip);
    }

    // Cambiar el volumen de la música de fondo
    public void SetMusicVolume(float volume)
    {
        backgroundMusicSource.volume = Mathf.Clamp01(volume);
    }

    // Cambiar el volumen de los efectos de sonido
    public void SetSFXVolume(float volume)
    {
        soundEffectSource.volume = Mathf.Clamp01(volume);
    }
}
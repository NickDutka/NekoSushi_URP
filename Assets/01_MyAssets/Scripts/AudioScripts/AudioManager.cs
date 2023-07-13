using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance of the Audio Manager
    private static AudioManager instance;

    // Reference to audio sources
    public AudioSource backgroundMusicSource;
    public AudioSource[] soundEffectSources;


    // Play background music
    public void PlayBackgroundMusic(AudioClip clip, float volume)
    {
        backgroundMusicSource.clip = clip;
        backgroundMusicSource.volume = volume;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    // Play a sound effect
    public void PlaySoundEffect(AudioClip clip, AudioSource source, float volume)
    {
        source.clip = clip;
        source.volume = volume;
        source.Play();
    }

    // Stop background music
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    // Stop all sound effects
    public void StopAllSoundEffects()
    {
        foreach (AudioSource source in soundEffectSources)
        {
            source.Stop();
        }
    }
}


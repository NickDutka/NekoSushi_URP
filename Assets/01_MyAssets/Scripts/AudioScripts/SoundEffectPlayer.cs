using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource source;
    [SerializeField] private float volume = 1f;

    public void PlaySoundEffect()
    {
        audioManager.PlaySoundEffect(clip, source, volume);
    }
}

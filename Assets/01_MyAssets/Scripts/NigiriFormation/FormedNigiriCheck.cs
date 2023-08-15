using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormedNigiriCheck : MonoBehaviour
{

    public string checkTag = "Nigiri";
    public int nigiriCount = 8;
    public AudioClip triggerSound;
    public AudioClip winSound;

    private List<GameObject> objectsInsideTrigger = new List<GameObject>();
    public AudioSource completeSoundAudioSource;

    public ParticleSystem nigiriCompleteParticles;
    public ParticleSystem nigiriPlaceParticles;
    public FormingNigiriStepManager formingNigiriStepManager;
    public FormingNigiriDialogueManager formingNigiriDialogueManager;

    public bool nigiriComplete;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        // Add the entering object to the list if it has one of the specified tags
        if (other.CompareTag(checkTag))
        {
            objectsInsideTrigger.Add(other.gameObject);
            PlayTriggerSound();
            nigiriPlaceParticles.Play();
        }

        // Check specific objects are now inside the trigger
        if (objectsInsideTrigger.Count == nigiriCount && nigiriComplete == false)
        {
            Debug.Log("All slice objects are inside the trigger.");
            PlayWinSound();
            nigiriCompleteParticles.Play();
            nigiriComplete = true;

            formingNigiriDialogueManager.MoveForward();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // Remove the exiting object from the list
        if (other.CompareTag(checkTag))
        {
            objectsInsideTrigger.Remove(other.gameObject);
        }
    }

    private void PlayTriggerSound()
    {
        if (completeSoundAudioSource != null && triggerSound != null)
        {
            completeSoundAudioSource.PlayOneShot(triggerSound);
        }
    }
    private void PlayWinSound()
    {
        if (completeSoundAudioSource != null && winSound != null)
        {
            completeSoundAudioSource.PlayOneShot(winSound);
        }
    }
}

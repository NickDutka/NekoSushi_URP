using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormedNigiriCheck : MonoBehaviour
{

    public string checkTag = "Nigiri";

    public AudioClip triggerSound;

    private List<GameObject> objectsInsideTrigger = new List<GameObject>();
    public AudioSource completeSoundAudioSource;

    public ParticleSystem nigiriCompleteParticles;
    public FormingNigiriStepManager formingNigiriStepManager;

    public bool nigiriComplete;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        // Add the entering object to the list if it has one of the specified tags
        if (other.CompareTag(checkTag))
        {
            objectsInsideTrigger.Add(other.gameObject);
        }

        // Check specific objects are now inside the trigger
        if (objectsInsideTrigger.Count == 8 && nigiriComplete == false)
        {
            Debug.Log("All slice objects are inside the trigger.");
            PlayTriggerSound();
            nigiriCompleteParticles.Play();
            nigiriComplete = true;

            formingNigiriStepManager.formingStep = 4;
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
}

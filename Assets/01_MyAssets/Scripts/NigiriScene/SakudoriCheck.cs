using UnityEngine;
using System.Collections.Generic;

public class SakudoriCheck : MonoBehaviour
{
    public string checkTag = "SlicedObject";
    public AudioClip triggerSound; // Assign the sound clip in the Inspector

    private List<GameObject> objectsInsideTrigger = new List<GameObject>();
    public AudioSource completeSoundAudioSource;

    public ParticleSystem sakudoriCompleteParticles;

    public NigiriStepManager nigiriStepManager;

    public bool sakudoriComplete;

    private void OnTriggerEnter(Collider other)
    {
        // Add the entering object to the list if it has one of the specified tags
        if (other.CompareTag(checkTag))
        {
            objectsInsideTrigger.Add(other.gameObject);
        }

        // Check if both specific objects are now inside the trigger
        if (objectsInsideTrigger.Count == 2 && sakudoriComplete == false)
        {
            Debug.Log("Both specific objects are inside the trigger.");
            PlayTriggerSound(); // Call the method to play the sound

            sakudoriCompleteParticles.Play();

            sakudoriComplete = true;
            nigiriStepManager.nigiriStep = 2;
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
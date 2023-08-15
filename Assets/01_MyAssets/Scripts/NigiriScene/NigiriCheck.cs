using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NigiriCheck : MonoBehaviour
{
    public string checkTag = "SlicedObject";
    public AudioClip triggerSound; // Assign the sound clip in the Inspector

    private List<GameObject> objectsInsideTrigger = new List<GameObject>();
    public AudioSource completeSoundAudioSource;

    public ParticleSystem nigiriCompleteParticles;

    public NigiriStepManager nigiriStepManager;

    public bool salmonNigiriComplete;
    public bool tunaNigiriComplete;

    private void OnTriggerEnter(Collider other)
    {
        // Add the entering object to the list if it has one of the specified tags
        if (other.CompareTag(checkTag))
        {
            objectsInsideTrigger.Add(other.gameObject);
        }

        // Check if both specific objects are now inside the trigger
        if (objectsInsideTrigger.Count == 4 && salmonNigiriComplete == false)
        {
            Debug.Log("All slice objects are inside the trigger.");
            PlayTriggerSound();
            nigiriCompleteParticles.Play();
            salmonNigiriComplete = true;
            foreach (GameObject obj in objectsInsideTrigger)
            {
                obj.tag = "SalmonSlice";
            }
            nigiriStepManager.nigiriStep = 4;
        }
        if(objectsInsideTrigger.Count == 8 && tunaNigiriComplete == false)
        {
            Debug.Log("All slice objects are inside the trigger.");
            PlayTriggerSound();
            nigiriCompleteParticles.Play();
            tunaNigiriComplete = true;
            foreach (GameObject obj in objectsInsideTrigger)
            {
                obj.tag = "TunaSlice";
            }
            nigiriStepManager.nigiriStep = 8;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Remove the exiting object from the list
        if (other.CompareTag(checkTag))
        {
            objectsInsideTrigger.Remove(other.gameObject);
        }

        // Trigger Nigiri slicing instruction when one of the objects exits the trigger
        //if (other.CompareTag(checkTag) && nigiriComplete == true &&
        //    nigiriStepManager.nigiriStep == 4)
        //{
        //    nigiriStepManager.nigiriStep = 5;
        //}
    }

    private void PlayTriggerSound()
    {
        if (completeSoundAudioSource != null && triggerSound != null)
        {
            completeSoundAudioSource.PlayOneShot(triggerSound);
        }
    }
}

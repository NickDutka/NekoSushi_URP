using UnityEngine;
using System.Collections.Generic;

public class SakudoriCheck : MonoBehaviour
{
    public string checkTag = "SlicedObject";
    public AudioClip triggerSound; // Assign the sound clip in the Inspector

    private List<GameObject> objectsInsideTrigger = new List<GameObject>();
    private List<SlicedObjectData> slicedObjectDataList = new List<SlicedObjectData>();
    public AudioSource completeSoundAudioSource;

    public ParticleSystem sakudoriCompleteParticles;

    public NigiriStepManager nigiriStepManager;

    public bool salmonSakudoriComplete;
    public bool tunaSakudoriComplete;

    private void OnTriggerEnter(Collider other)
    {
        // Add the entering object to the list if it has one of the specified tags
        if (other.CompareTag(checkTag))
        {
            objectsInsideTrigger.Add(other.gameObject);

            other.gameObject.GetComponent<OnDestroyEvent>().OnObjectDestroyed += RemoveObjectFromList;
        }

        // Check if both specific objects are now inside the trigger
        if (objectsInsideTrigger.Count == 2 && salmonSakudoriComplete == false)
        {
            Debug.Log("Both specific objects are inside the trigger.");
            PlayTriggerSound(); // Call the method to play the sound

            sakudoriCompleteParticles.Play();

            salmonSakudoriComplete = true;
            nigiriStepManager.nigiriStep = 2;

            slicedObjectDataList.Clear();

            foreach (GameObject obj in objectsInsideTrigger)
            {
                SlicedObjectData data = new SlicedObjectData(obj);
                slicedObjectDataList.Add(data);
                Debug.Log("Added data for " + obj.name);
            }
        }

        if(objectsInsideTrigger.Count == 4 && tunaSakudoriComplete == false)
        {
            Debug.Log("four specific objects are inside the trigger.");
            PlayTriggerSound(); // Call the method to play the sound

            sakudoriCompleteParticles.Play();

            tunaSakudoriComplete = true;
            nigiriStepManager.nigiriStep = 6;

            slicedObjectDataList.Clear();

            foreach (GameObject obj in objectsInsideTrigger)
            {
                SlicedObjectData data = new SlicedObjectData(obj);
                slicedObjectDataList.Add(data);
                Debug.Log("Added data for " + obj.name);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // Remove the exiting object from the list
        if (other.CompareTag(checkTag))
        {
            objectsInsideTrigger.Remove(other.gameObject);
        }

        // Trigger Salmon Nigiri slicing instruction when one of the objects exits the trigger
        if(other.CompareTag(checkTag) && salmonSakudoriComplete == true && 
            nigiriStepManager.nigiriStep == 2)
        {
            nigiriStepManager.nigiriStep = 3;
        }
        // Trigger Tuna Nigiri slicing instruction when one of the objects exits the trigger
        if(other.CompareTag(checkTag) && tunaSakudoriComplete == true &&
                       nigiriStepManager.nigiriStep == 6)
        {
            nigiriStepManager.nigiriStep = 7;
        }
    }
    private void RemoveObjectFromList(GameObject obj)
    {
        // Remove the destroyed object from the list
        objectsInsideTrigger.Remove(obj);
    }
    private void PlayTriggerSound()
    {
        if (completeSoundAudioSource != null && triggerSound != null)
        {
            completeSoundAudioSource.PlayOneShot(triggerSound);
        }
    }

    // Method to spawn objects from the stored data list

    public void SpawnObjectsFromStoredData()
    {
        foreach (SlicedObjectData data in slicedObjectDataList)
        {
            // Create a new empty GameObject
            GameObject newObject = new GameObject("NewObject");

            // Set the position and rotation using the stored data
            newObject.transform.position = data.position;
            newObject.transform.rotation = data.rotation;

            // Set the scale using the stored data
            newObject.transform.localScale = data.scale;

            // Set layer and tag using the stored data
            newObject.layer = data.layerNum;
            newObject.tag = data.tagName;

            // Check other properties and add components/scripts if required
            if (data.hasMeshFilter && newObject.GetComponent<MeshFilter>() == null)
            {
                newObject.AddComponent<MeshFilter>();
            }

            if (data.mesh != null)
            {
                MeshFilter meshFilter = newObject.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    meshFilter.sharedMesh = data.mesh;
                }
            }
            // Set up the MeshRenderer component and assign materials
            if (data.hasMeshRenderer)
            {
                MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();

                // Assign materials if they exist in the stored data
                if (data.materials != null && data.materials.Length > 0)
                {
                    meshRenderer.materials = data.materials;
                }
                else
                {
                    // If materials are not available, you can assign a default material or do other handling here.
                    // Example:
                    // meshRenderer.material = defaultMaterial;
                }
            }
            if (data.hasRigidbody && newObject.GetComponent<Rigidbody>() == null)
            {
                newObject.AddComponent<Rigidbody>();
            }
            // Set up the MeshCollider component and set it to convex
            if (data.hasMeshCollider)
            {
                MeshCollider meshCollider = newObject.AddComponent<MeshCollider>();
                meshCollider.sharedMesh = data.mesh;

                // Set the collider to convex
                meshCollider.convex = true;
            }

            if (data.hasGrabbableScript && newObject.GetComponent<Autohand.Grabbable>() == null)
            {
                newObject.AddComponent<Autohand.Grabbable>();
            }

            if (data.hasOnDestroyEventScript && newObject.GetComponent<OnDestroyEvent>() == null)
            {
                newObject.AddComponent<OnDestroyEvent>();
            }

            Debug.Log("Stored object Spawned.");    
            //newObject.layer = LayerMask.NameToLayer(data.layerName);
            // Add any other necessary setup here.
            // ...

            // Optionally, if the new object is meant to be part of a specific collection or group,
            // you can add it to another list or store it in a dictionary, depending on your requirements.
            // Example:
            // spawnedObjects.Add(newObject);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasabi : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finger")) // Replace "YourTagHere" with the tag of the objects you want to trigger the spawn.
        {
            // Instantiate the prefab at the same position and rotation as the object with the collider.
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

            // Make the spawned prefab a child of the object with the collider.
            spawnedPrefab.transform.parent = other.transform;
        }
    }

}

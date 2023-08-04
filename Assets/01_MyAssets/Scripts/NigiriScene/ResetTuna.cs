using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class ResetTuna : MonoBehaviour
{
    public NigiriStepManager nigiriStepManager;
    public SakudoriCheck sakudoriCheck;

    public GameObject prefabToSpawn; // The prefab you want to spawn
    public Transform spawnTransform; // The specific transform where you want to spawn the prefab

    public GameObject currentSpawnedPrefab; // Reference to the currently spawned prefab
    public ParticleSystem spawnParticles;
    public AudioSource resetSoundAudioSource;
    public AudioClip resetSound;

    // Call this function to delete the current prefab and spawn a new one at the specified transform
    public void DeleteAndSpawnNewTunaPrefab()
    {
        if (nigiriStepManager.nigiriStep == 5)
        {
            // Destroy any objects with the "SlicedObject" tag
            GameObject[] slicedObjects = GameObject.FindGameObjectsWithTag("SlicedObject");
            foreach (GameObject slicedObject in slicedObjects)
            {
                Destroy(slicedObject);
            }
            // Check if there's already a spawned prefab and destroy it if it exists
            if (currentSpawnedPrefab != null)
            {
                Destroy(currentSpawnedPrefab);
            }
            spawnParticles.Play();
            resetSoundAudioSource.PlayOneShot(resetSound);
            // Spawn the new prefab at the specified transform
            currentSpawnedPrefab = Instantiate(prefabToSpawn, spawnTransform.position, spawnTransform.rotation);
        }

        if (nigiriStepManager.nigiriStep == 7)
        {
            // Destroy any objects with the "SlicedObject" tag
            GameObject[] slicedObjects = GameObject.FindGameObjectsWithTag("SlicedObject");
            foreach (GameObject slicedObject in slicedObjects)
            {
                Destroy(slicedObject);
            }

            spawnParticles.Play();
            resetSoundAudioSource.PlayOneShot(resetSound);
            sakudoriCheck.SpawnObjectsFromStoredData();
        }

    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(ResetTuna))]
public class ActivateTunaSpawnButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ResetTuna vrShootInput = (ResetTuna)target;

        if (GUILayout.Button("Delete and Spawn"))
        {
            vrShootInput.DeleteAndSpawnNewTunaPrefab();
        }

    }

}
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class KnifeLocker : MonoBehaviour
{
    public StepManagerIntroScene stepManagerIntroScene;
    public GameObject[] knives;

    private bool isUnlocked = false;

    private void Start()
    {
        // Disable the Grabbable component for all knives at the beginning
        foreach (GameObject knife in knives)
        {
            knife.GetComponent<Grabbable>().enabled = false;
        }
    }

    private void Update()
    {
        if (stepManagerIntroScene.currentStep == 2 && !isUnlocked)
        {
            // When step 2 is reached and the knives are not yet unlocked, unlock them
            UnlockKnives();
        }
    }

    private void UnlockKnives()
    {
        // Enable the Grabbable component for all knives
        foreach (GameObject knife in knives)
        {
            knife.GetComponent<Grabbable>().enabled = true;
        }

        // Mark the knives as unlocked to prevent repeated enabling
        isUnlocked = true;
    }
}
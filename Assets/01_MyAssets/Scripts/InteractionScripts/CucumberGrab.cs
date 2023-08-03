using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberGrab : MonoBehaviour
{
    public StepManagerIntroScene stepManagerIntroScene;

    public AudioSource cucumberAudio;
    public void InitializeCucumber()
    {
        if (stepManagerIntroScene != null)
        {
            if (stepManagerIntroScene.currentStep == 5)
            {
                stepManagerIntroScene.MoveForward();

                PlaySound();

            }
        }
    }

    public void PlaySound()
    {
        if (cucumberAudio != null)
        {
            cucumberAudio.Play(); // Play the sound
        }
    }
}

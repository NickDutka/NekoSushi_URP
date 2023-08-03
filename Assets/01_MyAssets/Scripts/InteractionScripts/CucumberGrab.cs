using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberGrab : MonoBehaviour
{
    public StepManagerIntroScene stepManagerIntroScene;

    public void InitializeCucumber()
    {
        if (stepManagerIntroScene != null)
        {
            if (stepManagerIntroScene.currentStep == 5)
            {
                stepManagerIntroScene.MoveForward();
            }
        }
    }
}

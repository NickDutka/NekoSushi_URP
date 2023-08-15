using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NigiriStepManager : MonoBehaviour
{
    public MySceneLoader sceneLoader;
    public StepManagerNigiriScene stepManagerNigiriScene;
    public SakudoriCheck sakudoriCheck;
    public FadePlayer fadePlayer;
    public int nigiriStep;
    public SliceObjectDistanceTraveled sliceObjectDistanceTraveled;
    public Material tunaMaterial;

    // Step flags
    public bool sakudoriInstructionStarted;
    public bool nigiriInstructionStarted;
    public bool slicingInstructionStarted;
    public bool slicingInstructionComplete;
    public bool tunaSakudoriStarted;
    public bool tunaSakudoriInstructionComplete;
    public bool tunaNigiriInstructionStarted;
    public bool tunaNigiriInstructionComplete;

    public GameObject konroDialogueCanvas;

    //Salmon Specific UI
    public GameObject salmonSakudoriVideoCanvas;
    public GameObject salmonNigiriVideoCanvas;
    public GameObject salmonSakudoriHintPanel;
    public GameObject salmonNigiriHintPanel;
    public GameObject salmonResetButton;
    //Tuna Specific UI
    public GameObject tunaSakudoriVideoCanvas;
    public GameObject tunaNigiriVideoCanvas;
    public GameObject tunaSakudoriHintPanel;
    public GameObject tunaNigiriHintPanel;
    public GameObject tunaResetButton;

    public GameObject startingTunaFillet;
    public GameObject nigiriTray;
    public GameObject uiButton;


    
    private void Start()
    {
        nigiriStep = 0;
        salmonSakudoriVideoCanvas.SetActive(false);
        salmonSakudoriHintPanel.SetActive(false);
        nigiriTray.SetActive(false);
    }

    private void Update()
    {
        // If the player grabs the salmon fillet, start the nigiri instruction
        if (nigiriStep == 1 && sakudoriInstructionStarted == false)
        {
            sakudoriInstructionStarted = true;
            StartSalmonSakudoriInstruction();
        }
        // If the player finishes the sakudori instruction, start the nigiri instruction
        if (nigiriStep == 2 && nigiriInstructionStarted == false)
        {
            nigiriInstructionStarted = true;
            StartSalmonNigiriInstruction();
            stepManagerNigiriScene.MoveForward();
        }
        // If the player finishes the nigiri instruction, start the slicing instruction
        if (nigiriStep == 3 && slicingInstructionStarted == false)
        {
            slicingInstructionStarted = true;
            StartSalmonSlicingInstruction();
        }
        // If the player finishes the slicing instruction, konro tells them to repeat with tuna
        if(nigiriStep == 4 && slicingInstructionComplete == false)
        {
            slicingInstructionComplete = true;
            SalmonSlicingInstructionComplete();
            stepManagerNigiriScene.MoveForward();
            GameObject[] leftoverSlices = GameObject.FindGameObjectsWithTag("SlicedObject");
            foreach(GameObject slice in leftoverSlices)
            {
                Destroy(slice);
            }
        }
        // If the player grabs the tuna fillet, start the tuna sakudori instruction
        if(nigiriStep == 5 && tunaSakudoriStarted == false)
        {
            tunaSakudoriStarted = true;
            sliceObjectDistanceTraveled.crossSectionMaterial = tunaMaterial;
            StartTunaSakudori();
        }
        // If the player finishes the tuna sakudori instruction, start the tuna nigiri instruction
        if(nigiriStep == 6 && tunaSakudoriInstructionComplete == false)
        {
            tunaSakudoriInstructionComplete = true;
            FinishedTunaSakudori();
            stepManagerNigiriScene.MoveForward(); // Nice work! Now slice the tuna for nigiri

        }
        if(nigiriStep == 7 && tunaNigiriInstructionStarted == false)
        {
            tunaNigiriInstructionStarted = true;
            StartTunaSlicingInstruction();
        }
        if(nigiriStep == 8 && tunaNigiriInstructionComplete == false)
        {
            tunaNigiriInstructionComplete = true;
            FinishedTunaSlicingInstruction();
            stepManagerNigiriScene.MoveForward();
            LoadNextScene();
        }

    }
    public void LoadNextScene()
    {
        fadePlayer.Invoke("StartFadeToBlack", 5f);
        sceneLoader.Invoke("LoadNextScene", 10f);
        //Load next scene
    }
    public void StartSalmonSakudoriInstruction()
    {
        // If the player grabs the salmon fillet, start the nigiri instruction
        konroDialogueCanvas.SetActive(false);
        salmonSakudoriVideoCanvas.SetActive(true);
        salmonSakudoriHintPanel.SetActive(true);

        uiButton.SetActive(false);
        salmonResetButton.SetActive(true);   
    }

    public void StartSalmonNigiriInstruction()
    {
        // If player finishes the sakudori instruction, start the nigiri instruction
        konroDialogueCanvas.SetActive(true);
        salmonSakudoriVideoCanvas.SetActive(false);
        salmonSakudoriHintPanel.SetActive(false);

        //uiButton.SetActive(true);
        salmonResetButton.SetActive(false);
    }

    public void StartSalmonSlicingInstruction()
    {
        // If player finishes the nigiri instruction, start the slicing instruction
        konroDialogueCanvas.SetActive(false);
        salmonNigiriVideoCanvas.SetActive(true);
        salmonNigiriHintPanel.SetActive(true);

        uiButton.SetActive(false);
        salmonResetButton.SetActive(true);
        nigiriTray.SetActive(true);
    }

    public void SalmonSlicingInstructionComplete()
    {
        // If player finishes the slicing instruction, konro tells them to repeat with tuna
        konroDialogueCanvas.SetActive(true);
        salmonNigiriVideoCanvas.SetActive(false);
        salmonNigiriHintPanel.SetActive(false);

        //uiButton.SetActive(true);
        salmonResetButton.SetActive(false);
        startingTunaFillet.SetActive(true);
    }

    public void StartTunaSakudori()
    {
        // If player grabs the tuna fillet, start the tuna sakudori instruction
        konroDialogueCanvas.SetActive(false);
        tunaSakudoriVideoCanvas.SetActive(true);
        tunaSakudoriHintPanel.SetActive(true);

        uiButton.SetActive(false);
        tunaResetButton.SetActive(true);
    }

    public void FinishedTunaSakudori()
    {
        konroDialogueCanvas.SetActive(true);
        tunaSakudoriVideoCanvas.SetActive(false);
        tunaSakudoriHintPanel.SetActive(false);
    }

    public void StartTunaSlicingInstruction()
    {
        konroDialogueCanvas.SetActive(false);
        tunaNigiriVideoCanvas.SetActive(true);
        salmonNigiriHintPanel.SetActive(true);

    }

    public void FinishedTunaSlicingInstruction()
    {
        konroDialogueCanvas.SetActive(true);
        tunaNigiriVideoCanvas.SetActive(false);
        tunaNigiriHintPanel.SetActive(false);
    }
}

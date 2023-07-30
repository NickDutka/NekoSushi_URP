using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NigiriStepManager : MonoBehaviour
{

    public int nigiriStep;

    public bool instructionStarted;

    public GameObject konroDialogueCanvas;
    public GameObject videoInstructionCanvas;
    public GameObject hintPanel;

    public GameObject uiButton;
    public GameObject resetButton;

    
    private void Start()
    {
        nigiriStep = 0;
        videoInstructionCanvas.SetActive(false);
        hintPanel.SetActive(false);
    }

    private void Update()
    {
        if (nigiriStep == 1 && instructionStarted == false)
        {
            instructionStarted = true;
            StartNigiriInstruction();
        }
    }
    public void StartNigiriInstruction()
    {
        // If the player grabs the salmon fillet, start the nigiri instruction
        konroDialogueCanvas.SetActive(false);
        videoInstructionCanvas.SetActive(true);
        hintPanel.SetActive(true);

        uiButton.SetActive(false);
        resetButton.SetActive(true);   
    }
}

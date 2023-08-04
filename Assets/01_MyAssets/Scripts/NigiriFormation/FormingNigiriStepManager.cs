using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormingNigiriStepManager : MonoBehaviour
{
    public FormingNigiriDialogueManager formingNigiriDialogueManager;
    public int formingStep;

    // step flags
    public bool nigiriIncstructionVideoStarted;

    //Video UI
    public GameObject nigiriIncstructionVideo;
    //Hint UI
    public GameObject hintPanel;
    //KonroUI
    public GameObject KonroUI;

    // Start is called before the first frame update
    void Start()
    {
        formingStep = 0;
        nigiriIncstructionVideo.SetActive(false);
        hintPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (formingStep == 3 && nigiriIncstructionVideoStarted == false)
        { 

            nigiriIncstructionVideoStarted = true;
            StartNigiriInstructionvideo();
            
        }
    }

    public void StartNigiriInstructionvideo()
    {
        Debug.Log("nigiri video started");

        KonroUI.SetActive(false);
        nigiriIncstructionVideo.SetActive(true);
        hintPanel.SetActive(true);  
    }
}

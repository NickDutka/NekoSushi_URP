using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormingNigiriStepManager : MonoBehaviour
{
    public MySceneLoader sceneLoader;   
    public FormingNigiriDialogueManager formingNigiriDialogueManager;
    public int formingStep;
    public KonroPath konroPath;
    public FadePlayer fadePlayer;

    // step flags
    public bool nigiriIncstructionVideoStarted;
    public bool nigiriComplete;
    public bool endSequence;

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

            konroPath.FollowInitialPath();
            
        }

        // end sequence
        if (formingStep == 4 && nigiriComplete == false)
        {
            nigiriComplete = true;
            Debug.Log("All slice objects are inside the trigger.");
            nigiriIncstructionVideo.SetActive(false);
            hintPanel.SetActive(false);
            //KonroUI.SetActive(true);
            konroPath.FollowReversePath();
            
            formingNigiriDialogueManager.nextButtonStage2.SetActive(true);
            formingNigiriDialogueManager.SetButtonState2(true);
        }

        if (formingStep == 5 && endSequence == false)
        {
            fadePlayer.StartFadeToBlack();
            sceneLoader.Invoke("LoadNextScene", 5f);
            // Load next scene
        }
    }

    public void StartNigiriInstructionvideo()
    {
        Debug.Log("nigiri video started");

        //KonroUI.SetActive(false);
        nigiriIncstructionVideo.SetActive(true);
        hintPanel.SetActive(true);  
    }

    public void LoadNextScene()
    {
        // Load next scene
    }

}

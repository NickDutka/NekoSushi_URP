using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoStepManager : MonoBehaviour
{
    public FadePlayer fadePlayer;
    public MySceneLoader sceneLoader;
    public NekoDialogueManager nekoDialogueManager;
    public int nekoStep;
    public DOTweenPath nekoPath;
    public DOTweenPath shipPath;

    public GameObject[] sushiPlatter;
    public ParticleSystem fx1;
    public ParticleSystem fx2;

    //Step flags
    public bool isShipMoving;
    public bool isNekoMoving;
    public bool isNekoDialogueStarted;
    public bool isNekoEating;
    public bool isFinishedEating;
    public bool isGameOver;

    //neko animations
    public NekoAnimationController nekoAnimator;

    
    // Start is called before the first frame update
    void Start()
    {
        nekoStep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(nekoStep == 1 && isShipMoving == false)
        {
            isShipMoving = true;
            shipPath.DOPlay();
        }

        if(nekoStep == 2 && isNekoDialogueStarted == false)
        {
            //.... So, you're the new "Chef", eh!
            isNekoDialogueStarted = true;
            nekoDialogueManager.nameText.text = "Neko";
            

            //set the dialogue box to the neko's position and parent it to the neko
            nekoDialogueManager.mainUICanvas.transform.position = 
                nekoDialogueManager.nekoUIAnchor.transform.position;
            nekoDialogueManager.mainUICanvas.transform.rotation =
                nekoDialogueManager.nekoUIAnchor.transform.rotation;
            nekoDialogueManager.mainUICanvas.transform.parent =
                nekoDialogueManager.nekoUIAnchor.transform;

        }

        if(nekoStep == 5 && isNekoEating == false)
        {
            isNekoEating = true;
            // play the eating animation
            nekoAnimator.SetEatingState(true);    

            // disable button
            // coroutine to go through steps 6-8
            StartCoroutine(nekoDialogueManager.NekoEatingUpdateSteps());
            nekoDialogueManager.physicsGadgetButton.enabled = false;
            nekoDialogueManager.SetButtonState(false);
        }
        if(nekoStep == 8 && isFinishedEating == false)
        {

            isFinishedEating = true;
            nekoAnimator.SetEatingState(false);
            nekoAnimator.SetHappyState(true);
            
            fx1.Play();
            fx2.Play();
            foreach(GameObject sushi in sushiPlatter)
            {
                sushi.SetActive(false);
            }
            
        }
        if(nekoStep == 10 && isGameOver == false)
        {
            isGameOver = true;

            fadePlayer.Invoke("StartFadeToBlack", 5f);

            sceneLoader.Invoke("LoadFirstScene", 10f);
        }
        

    }

}

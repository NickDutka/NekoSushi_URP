using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Oculus.Interaction;

public class StartTween : MonoBehaviour
{
    public KumaDialogue kumaDialogue;
    public GameObject kumaUISetup;

    public GameObject PlayerGO;
    public FadePlayer fadePlayer;
    public Transform teleAnchor;

    public GameObject pandaGO;
    public float rotationSpeed;
    private DOTweenPath dotweenPath;
    public Animator pandaAnimator;
    public bool isWalking;
    public bool isRunning;
    public bool isWaving;
    public bool isYes;
    public bool isNo;
    public bool isIdle;


    public bool isWalkingAway = false;

    private bool isRotating = false; // Add a flag to indicate rotation

    private Quaternion targetRotation = Quaternion.identity;
    private void Start()
    {
        // Get the reference to the DOTweenPath component
        dotweenPath = GetComponent<DOTweenPath>();

        //Invoke("StartPathMovementFromScript", 5);
    }
    private void Update()
    {
        

        //Kuma is done talking and heads up the hill
        if(kumaDialogue.currentStep == 4)
        {
            //Kuma is done talking and heads up the hill
            if (kumaDialogue.currentStep == 4 && !isWalkingAway)
            {
                isWalkingAway = true;
                StartPathMovementFromScript();
                SetWalkingState(true);
            }
        }

        if (isRotating)
        {
            // Continuously update the rotation until it reaches the targetRotation
            pandaGO.transform.rotation = Quaternion.Slerp(pandaGO.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check if the rotation is close enough to the targetRotation to stop rotating
            if (Quaternion.Angle(pandaGO.transform.rotation, targetRotation) < 0.01f)
            {
                pandaGO.transform.rotation = targetRotation;
                isRotating = false; // Set the flag to false when the rotation is complete
            }
        }
    }

    public void PathComplete()
    {

        SetWalkingState(false);

        targetRotation = Quaternion.Euler(0f, 0f, 0f);
        isRotating = true; // Set the flag to true when we want to start rotating
        kumaUISetup.SetActive(true);

        StartCoroutine(TeleportPlayer());

        kumaDialogue.MoveForward();

        Debug.Log("PathComplete");

        
    }

    private System.Collections.IEnumerator TeleportPlayer()
    {
        fadePlayer.StartFadeToBlack();

        yield return new WaitForSeconds(5);

        PlayerGO.transform.position = teleAnchor.position;
        PlayerGO.transform.rotation = teleAnchor.rotation;

        fadePlayer.StartFadeFromBlack();
    }

    public void StartPathMovementFromScript()
    {
        // Start the movement of the GameObject along the path
        dotweenPath.DOPlay();
        kumaUISetup.SetActive(false);

    }

    private void SetWalkingState(bool state)
    {
        pandaAnimator.SetBool("isWalking", state);
        isWalking= state;
    }
    private void SetRunningState(bool state)
    {
        pandaAnimator.SetBool("isRunning", state);
        isRunning = state;
    }
    private void SetWavingState(bool state)
    {
        pandaAnimator.SetBool("isWaving", state);
        isWaving = state;
    }
    private void SetYesState(bool state)
    {
        pandaAnimator.SetBool("isYes", state);
        isYes = state;
    }
    private void SetNoState(bool state)
    {
        pandaAnimator.SetBool("isNo", state);
        isNo = state;
    }
    private void SetIdleState(bool state)
    {
        pandaAnimator.SetBool("isIdle", state);
        isIdle = state;
    }

}

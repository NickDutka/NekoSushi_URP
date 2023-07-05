using System.Collections;
using UnityEngine;

public class KonroAnimationController : MonoBehaviour
{
    private Animator animator;
    private float nextBlinkTime;
    [SerializeField] bool isBlinking;
    [SerializeField] bool isAngry;
    [SerializeField] bool isTalking;
    [SerializeField] bool isPointTalking;

    private int blinkLayerIndex;
    private int baseLayerIndex;

    [SerializeField] KonroDialogueUI konroDialogueUI;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        blinkLayerIndex = animator.GetLayerIndex("Blink");
        baseLayerIndex = animator.GetLayerIndex("Base Layer");

        SetAngryState(false);
        SetBlinkState(false);
        SetTalkingState(false);

        SetRandomBlinkTime();
    }

    private void Update()
    {
        // Check if it's not angry and time to trigger a blink
        if (!isAngry && !isBlinking && Time.time >= nextBlinkTime)
        {
            Debug.Log("Check if it's not angry and time to trigger a blink");
            SetBlinkState(true);
            StartCoroutine(CheckBlinkCompletion());
        }
        //
        if (konroDialogueUI.currentStep < 2)
        {
            SetTalkingState(true);
        }

        // Telling you to grab Nakiri
        if (konroDialogueUI.currentStep == 2)
        {
            SetAngryState(false);
            SetTalkingState(false);
            SetPointTalkingState(true);
        }
        // If you grab wrong knife, konro gets angry
        if (konroDialogueUI.currentStep == 3)
        {
            SetPointTalkingState(false);
            SetAngryState(true);
        }
        // If you grab the right knife, konro is happy
        if (konroDialogueUI.currentStep >= 4)
        {
            SetAngryState(false);
            SetPointTalkingState(false);
            SetTalkingState(true);
        }
        // Telling you to cut cucumbers
        if (konroDialogueUI.currentStep == 5)
        {
            SetTalkingState(false);
            SetPointTalkingState(true);
        }

        // Check if it's not angry and the space key is pressed
        if(!isAngry && Input.GetKeyDown(KeyCode.Space))
        {
            SetAngryState(true);
        }
        // Check if it's angry and the space key is pressed
        else if(isAngry && Input.GetKeyDown(KeyCode.Space))
        {
            SetAngryState(false);
        }
        // Check if it's not talking and the T key is pressed
        if(!isTalking && Input.GetKeyDown(KeyCode.T))
        {
            SetTalkingState(true);
        }
        // Check if it's talking and the T key is pressed
        else if (isTalking && Input.GetKeyDown(KeyCode.T))
        {
            SetTalkingState(false);
        }   
    }

    private void SetBlinkState(bool state)
    {
        animator.SetBool("isBlinking", state);
        isBlinking = state;
    }
    private void SetAngryState(bool state)
    {
        animator.SetBool("isAngry", state);
        isAngry = state;
    }
    private void SetTalkingState(bool state)
    {
        animator.SetBool("isTalking", state);
        isTalking = state;
    }

    private void SetPointTalkingState(bool state)
    {
        animator.SetBool("isPointTalking", state);
        isPointTalking = state;
    }
    
    private IEnumerator CheckBlinkCompletion()
    {
        //yield return new WaitForEndOfFrame(); // Wait until the end of the frame to ensure the animation starts playing

        // Wait for the transition to end
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(blinkLayerIndex).normalizedTime <= 1.0f );

        // Wait for the transition to end
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(blinkLayerIndex).normalizedTime <= 1.0f);

        // Wait until the blink animation has finished playing
        //yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(blinkLayerIndex).IsName("Blink"));

        Debug.Log("Blink finished");
        SetBlinkState(false);
        SetRandomBlinkTime();
    }

    private void SetRandomBlinkTime()
    {
        nextBlinkTime = Time.time + Random.Range(1f, 10f);
    }
}
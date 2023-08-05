using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoAnimationController : MonoBehaviour
{
    private Animator animator;
    private float nextBlinkTime;
    [SerializeField] bool isBlinking;
    [SerializeField] bool isTalking;
    [SerializeField] bool isHappy;
    [SerializeField] bool isEating;

    private int blinkLayerIndex;
    private int baseLayerIndex;



    private void Awake()
    {

        animator = GetComponent<Animator>();

        blinkLayerIndex = animator.GetLayerIndex("Blink");
        baseLayerIndex = animator.GetLayerIndex("Base Layer");

        SetBlinkState(false);
        SetTalkingState(false);
        SetHappyState(false);
        SetEatingState(false);


        SetRandomBlinkTime();
    }

    private void Update()
    {

        // Check if it's not angry and time to trigger a blink
        if (!isEating && !isHappy && !isBlinking && Time.time >= nextBlinkTime)
        {
            //Debug.Log("Check if it's not angry and time to trigger a blink");
            SetBlinkState(true);
            StartCoroutine(CheckBlinkCompletion());
        }

        if(!isHappy && Input.GetKeyDown(KeyCode.Space))
        {
            SetHappyState(true);
        }
        else if (isHappy && Input.GetKeyDown(KeyCode.Space))
        {
            SetHappyState(false);
        }
        if(!isEating && Input.GetKeyDown(KeyCode.E))
        {
            SetEatingState(true);
        }
        else if (isEating && Input.GetKeyDown(KeyCode.E))
        {
            SetEatingState(false);
        }


    }

    public void SetBlinkState(bool state)
    {
        animator.SetBool("isBlinking", state);
        isBlinking = state;
    }
    public void SetTalkingState(bool state)
    {
        animator.SetBool("isTalking", state);
        isTalking = state;
    }
    public void SetHappyState(bool state)
    {
        animator.SetBool("isHappy", state);
        isHappy = state;
    }
    public void SetEatingState(bool state)
    {
        animator.SetBool("isEating", state);
        isEating = state;
    }

    private IEnumerator CheckBlinkCompletion()
    {
        //yield return new WaitForEndOfFrame(); // Wait until the end of the frame to ensure the animation starts playing

        // Wait for the transition to end
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(blinkLayerIndex).normalizedTime <= 1.0f);

        // Wait for the transition to end
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(blinkLayerIndex).normalizedTime <= 1.0f);

        // Wait until the blink animation has finished playing
        //yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(blinkLayerIndex).IsName("Blink"));

        //Debug.Log("Blink finished");
        SetBlinkState(false);
        SetRandomBlinkTime();
    }

    private void SetRandomBlinkTime()
    {
        nextBlinkTime = Time.time + Random.Range(1f, 5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonroBaseAnimController : MonoBehaviour
{
    public Animator animator;
    private float nextBlinkTime;
    public bool isBlinking;
    public bool isAngry;
    public bool isTalking;
    public bool isPointTalking;

    private int blinkLayerIndex;
    private int baseLayerIndex;

    private void Awake()
    {
        blinkLayerIndex = animator.GetLayerIndex("Blink");
        baseLayerIndex = animator.GetLayerIndex("Base Layer");

        SetRandomBlinkTime();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetAngryState(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBlinkState(bool state)
    {
        animator.SetBool("isBlinking", state);
        isBlinking = state;
    }
    public void SetAngryState(bool state)
    {
        animator.SetBool("isAngry", state);
        isAngry = state;
    }
    public void SetTalkingState(bool state)
    {
        animator.SetBool("isTalking", state);
        isTalking = state;
    }

    public void SetPointTalkingState(bool state)
    {
        animator.SetBool("isPointTalking", state);
        isPointTalking = state;
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

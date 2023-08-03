using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Autohand;
using System.ComponentModel;
using System;

public class KumaDialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText; // Reference to the TextMeshProUGUI component displaying the instructions
    [SerializeField] private StepInstructionsSO stepInstructionsSO; // Reference to the StepInstructionsSO scriptable object
    [SerializeField] private float fadeDuration = 0.5f; // Duration of the fade animation in seconds

    public int currentStep = 0; // Variable to track the current step index
    [SerializeField] private CanvasGroup canvasGroup; // Reference to the CanvasGroup component for fading animation
    public GameObject nextButtonStage1; // Reference to the next button
    public GameObject nextButtonStage2; // Reference to the next button

    [SerializeField] private PhysicsGadgetButton physicsGadgetButton;
    [SerializeField] private PhysicsGadgetButton physicsGadgetButton2;

    [SerializeField] private bool continueDialogue = false;
    [SerializeField] private bool buttonIsActive = true;
    [SerializeField] private float dialogueTimeDelay = 5f;

    [SerializeField] private Image[] nextArrowImages;

    [SerializeField] private float arrowfadeDuration = 0.5f;
    [SerializeField] private float pauseDuration = 0.5f;
    [SerializeField] private float downarrowpause = 1f;

    [SerializeField] private Coroutine flashImagesCoroutine;

    [SerializeField] private Animator buttonAnimator;
    [SerializeField] private Animator buttonAnimator2;
    [SerializeField] private bool isButtonActive;
    [SerializeField] private bool isButtonActive2;

    [SerializeField] private AudioSource kumaAudio;
    [SerializeField] private AudioClip angry;
    [SerializeField] private AudioClip[] talking;
    [SerializeField] private AudioClip happy;
    [SerializeField] private AudioClip pointTalking;


    public void SetButtonState(bool state)
    {
        buttonAnimator.SetBool("isButtonActive", state);
        isButtonActive = state;

    }
    public void SetButtonState2(bool state)
    {
        buttonAnimator2.SetBool("isButtonActive", state);
        isButtonActive2 = state;

    }
    // Start is called before the first frame update
    private void Start()
    {


        UpdateDialogueText(); // Initial update of the instruction text and image
        physicsGadgetButton.enabled = false;
        SetButtonState(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (continueDialogue == false && flashImagesCoroutine != null)
        {
            StopCoroutine(flashImagesCoroutine);
            physicsGadgetButton.enabled = false;
            physicsGadgetButton2.enabled = false;

        }
    }

    private void UpdateDialogueText()
    {

        continueDialogue = false;
        StartCoroutine(DialogueTimeDelay());

        for (int i = 0; i < nextArrowImages.Length; i++)
        {
            nextArrowImages[i].enabled = false;
        }

        StartCoroutine(FadeOut(() =>
        {
            dialogueText.text = stepInstructionsSO.Instructions[currentStep]; // Update the instruction text with the current step's instruction
            StartCoroutine(FadeIn()); // Start the fade-in coroutine after the fade-out is complete

        }));
    }

    public void MoveForward()
    {
        if (currentStep < stepInstructionsSO.Instructions.Length - 1)
        {
            currentStep++; // Increment the current step index if there are more steps available
            int randomIndex = UnityEngine.Random.Range(0, talking.Length); // Generate a random index within the range of the array length
            kumaAudio.PlayOneShot(talking[randomIndex]);
            UpdateDialogueText(); // Update the instruction text
            Debug.Log("MoveForward called. Current step: " + currentStep);
        }
    }

    private IEnumerator DialogueTimeDelay()
    {
        yield return new WaitForSeconds(dialogueTimeDelay);

        continueDialogue = true;

        if (continueDialogue == true)
        {
            physicsGadgetButton.enabled = true;;
            physicsGadgetButton2.enabled = true;

            for (int i = 0; i < nextArrowImages.Length; i++)
            {
                nextArrowImages[i].enabled = true;
            }

            flashImagesCoroutine = StartCoroutine(FlashImages());

        }

    }
    public IEnumerator ButtonDisableDelay()
    {
        yield return new WaitForSeconds(3f);
        nextButtonStage1.SetActive(false);
        nextButtonStage2.SetActive(false);
    }

    private System.Collections.IEnumerator FadeOut(System.Action onComplete)
    {
        float elapsedTime = 0f;
        float startAlpha = 1f;
        float targetAlpha = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        onComplete?.Invoke(); // Invoke the onComplete action (update the text and image)
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float startAlpha = 0f;
        float targetAlpha = 1f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }
    }
    private float[] fadeProgress; // Declaration of fadeProgress variable
    private IEnumerator FlashImages()
    {
        fadeProgress = new float[nextArrowImages.Length]; // Initialize fadeProgress array

        while (true)
        {
            // Fade in all images simultaneously
            for (int i = 0; i < nextArrowImages.Length; i++)
            {
                Image image = nextArrowImages[i];

                if (fadeProgress[i] < 1f)
                {
                    Color startColor = image.color;
                    startColor.a = 0f;
                    image.color = startColor;

                    StartCoroutine(FadeImage(image, 0f, 1f, arrowfadeDuration, i)); // Start fade coroutine for the image
                }
            }

            yield return new WaitForSeconds(arrowfadeDuration); // Wait for the fade in to complete

            // Fade out all images simultaneously
            for (int i = 0; i < nextArrowImages.Length; i++)
            {
                Image image = nextArrowImages[i];

                if (fadeProgress[i] >= 1f)
                {
                    StartCoroutine(FadeImage(image, 1f, 0f, arrowfadeDuration, i)); // Start fade coroutine for the image
                }
            }

            yield return new WaitForSeconds(arrowfadeDuration); // Wait for the fade out to complete

            // Reset fade progress for the next iteration
            Array.Clear(fadeProgress, 0, fadeProgress.Length);
        }
    }

    private IEnumerator FadeImage(Image image, float startAlpha, float endAlpha, float duration, int index)
    {
        Color startColor = image.color;
        Color endColor = image.color;
        startColor.a = startAlpha;
        endColor.a = endAlpha;

        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / duration;
            Color currentColor = image.color;
            currentColor.a = Mathf.Lerp(startColor.a, endColor.a, t);
            image.color = currentColor;

            yield return null;
        }

        image.color = endColor;
        fadeProgress[index] = endAlpha;
    }

}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Autohand;
using System.ComponentModel;

public class KonroDialogueUI : MonoBehaviour
{
    public TMP_Text dialogueText; // Reference to the TextMeshProUGUI component displaying the instructions
    public StepInstructionsSO stepInstructionsSO; // Reference to the StepInstructionsSO scriptable object
    public float fadeDuration = 0.5f; // Duration of the fade animation in seconds

    public int currentStep = 0; // Variable to track the current step index
    public CanvasGroup canvasGroup; // Reference to the CanvasGroup component for fading animation
    public GameObject nextButtonStage1; // Reference to the next button
    public GameObject nextButtonStage2; // Reference to the next button

    public PhysicsGadgetButton physicsGadgetButton;
    public PhysicsGadgetButton physicsGadgetButton2;
    public bool continueDialogue = false;
    public bool buttonIsActive = true;
    public float dialogueTimeDelay = 5f;


    public Image[] nextArrowImages;
    public SpriteRenderer[] spriteRenderers;
    public float arrowfadeDuration = 0.5f;
    public float pauseDuration = 0.5f;

    Coroutine flashImagesCoroutine;
    Coroutine flashSpritesCoroutine;

    public Animator buttonAnimator;
    public Animator buttonAnimator2;
    [SerializeField] private bool isButtonActive;
    [SerializeField] private bool isButtonActive2;

    private void SetButtonState(bool state)
    {
        buttonAnimator.SetBool("isButtonActive", state);
        isButtonActive = state;

    }
    private void SetButtonState2(bool state)
    {
        buttonAnimator2.SetBool("isButtonActive", state);
        isButtonActive2 = state;

    }
    private void Start()
    {

        
        UpdateDialogueText(); // Initial update of the instruction text and image
        physicsGadgetButton.enabled = false;
        physicsGadgetButton2.enabled = false;
        SetButtonState(true);

    }
    private void Update()
    {
        //pick up the knife
        if (currentStep == 2 && buttonIsActive == true)
        {
            StartCoroutine(ButtonDisableDelay());
            buttonIsActive = false;
            SetButtonState(false);
        }
        //you have the knife
        if (currentStep == 4)
        {
            //SetButtonState(true);
            nextButtonStage2.SetActive(true);
            SetButtonState2(true);
            
        }
        // slice the cucumber
        if(currentStep == 5)
        {
            //nextButtonPostGrab.SetActive(false);
            StartCoroutine(ButtonDisableDelay());
            SetButtonState2(false);
            //SetButtonState(false);
        }

        if (continueDialogue == false && flashImagesCoroutine != null)
        {
            StopCoroutine(flashImagesCoroutine);
            StopCoroutine(flashSpritesCoroutine);
            physicsGadgetButton.enabled = false;
            physicsGadgetButton2.enabled = false;
        }
    }

    private void UpdateDialogueText()
    {

        continueDialogue = false;
        StartCoroutine(DialogueTimeDelay());

        nextArrowImages[0].enabled = false;
        spriteRenderers[0].enabled = false;

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
            //SetButtonState(false);
            currentStep++; // Increment the current step index if there are more steps available
            UpdateDialogueText(); // Update the instruction text
        }
    }
    public void FinishDialogueSequence()
    {
        if (currentStep < stepInstructionsSO.Instructions.Length - 1)
        {
            currentStep = 4; // Increment the current step index if there are more steps available
            UpdateDialogueText(); // Update the instruction text
        }
    }
    public void WrongKnifeSequence()
    {
        if (currentStep < stepInstructionsSO.Instructions.Length - 1)
        {
            currentStep = 3; // Increment the current step index if there are more steps available
            UpdateDialogueText(); // Update the instruction text
        }
    }
    public void SelectKnifeSequence()
    {
        if (currentStep < stepInstructionsSO.Instructions.Length - 1)
        {
            currentStep = 2; // Increment the current step index if there are more steps available
            UpdateDialogueText(); // Update the instruction text
        }
    }
    public void CucumberSequenceStart()
    {
        if (currentStep < stepInstructionsSO.Instructions.Length - 1)
        {
            currentStep = 5; // Increment the current step index if there are more steps available
            UpdateDialogueText(); // Update the instruction text
        }
    }

    public void MoveBackward()
    {
        if (currentStep > 0)
        {
            currentStep--; // Decrement the current step index if there are previous steps
            UpdateDialogueText(); // Update the instruction text
        }
    }
    
    IEnumerator DialogueTimeDelay()
    {
        yield return new WaitForSeconds(dialogueTimeDelay);

        continueDialogue = true;

        if(continueDialogue == true)
        {
            physicsGadgetButton.enabled = true;
            physicsGadgetButton2.enabled = true;
            nextArrowImages[0].enabled = true;
            spriteRenderers[0].enabled = true;
            flashImagesCoroutine = StartCoroutine(FlashImages());
            flashSpritesCoroutine = StartCoroutine(FlashSprites());
        }

    }
    IEnumerator ButtonDisableDelay()
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

    private IEnumerator FlashImages()
    {
        foreach (Image image in nextArrowImages)
        {
            Color startColor = image.color;
            startColor.a = 0f;
            image.color = startColor;

            yield return StartCoroutine(FadeImage(image, 0f, 1f, fadeDuration)); // Smoothly fade in the image
        }

        while (true)
        {
            foreach (Image image in nextArrowImages)
            {
                yield return StartCoroutine(FadeImage(image, 1f, 0f, fadeDuration));
                yield return new WaitForSeconds(pauseDuration);
                yield return StartCoroutine(FadeImage(image, 0f, 1f, fadeDuration));
                yield return new WaitForSeconds(pauseDuration);
            }
        }
    }

    private IEnumerator FadeImage(Image images, float startAlpha, float endAlpha, float duration)
    {

        Color startColor = images.color;
        Color endColor = images.color;
        startColor.a = startAlpha;
        endColor.a = endAlpha;

        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / duration;
            Color currentColor = images.color;
            currentColor.a = Mathf.Lerp(startColor.a, endColor.a, t);
            images.color = currentColor;
            yield return null;
        }

        images.color = endColor;
    }

    private IEnumerator FlashSprites()
    {

        // Set the initial alpha to 0 for all sprite renderers
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color startColor = spriteRenderer.color;
            startColor.a = 0f;
            spriteRenderer.color = startColor;

            yield return StartCoroutine(FadeSprite(spriteRenderer, 0f, 1f, fadeDuration)); // Smoothly fade in the sprite renderer
        }
        while (true)
        {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                yield return StartCoroutine(FadeSprite(spriteRenderer, 1f, 0f, fadeDuration));
                yield return new WaitForSeconds(pauseDuration);
                yield return StartCoroutine(FadeSprite(spriteRenderer, 0f, 1f, fadeDuration));
                yield return new WaitForSeconds(pauseDuration);
            }
        }
    }

    private IEnumerator FadeSprite(SpriteRenderer spriteRenderer, float startAlpha, float endAlpha, float duration)
    {
        Color color = spriteRenderer.color;
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / duration;
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            spriteRenderer.color = color;
            yield return null;
        }

        color.a = endAlpha;
        spriteRenderer.color = color;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KonroDialogueUI : MonoBehaviour
{
    public TMP_Text dialogueText; // Reference to the TextMeshProUGUI component displaying the instructions
    public StepInstructionsSO stepInstructionsSO; // Reference to the StepInstructionsSO scriptable object
    public float fadeDuration = 0.5f; // Duration of the fade animation in seconds

    public int currentStep = 0; // Variable to track the current step index
    public CanvasGroup canvasGroup; // Reference to the CanvasGroup component for fading animation
    public GameObject nextButton; // Reference to the next button
    public GameObject nextButtonPostGrab; // Reference to the next button

    public bool nexbuttonActive = true;

    private void Start()
    {

        UpdateDialogueText(); // Initial update of the instruction text and image
        
    }
    private void Update()
    {
        if (currentStep == 2)
        {
            nextButton.SetActive(false); // Disable the next button if there is only one step
            nexbuttonActive = false;

        }
        
        if (currentStep == 4)
        { 
            nextButtonPostGrab.SetActive(true);
        }

        if(currentStep == 5)
        {
            nextButtonPostGrab.SetActive(false);
        }
    }

    private void UpdateDialogueText()
    {
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
}
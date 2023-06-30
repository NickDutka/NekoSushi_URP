using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StepByStepUI : MonoBehaviour
{
    public TMP_Text instructionText; // Reference to the TextMeshProUGUI component displaying the instructions
    public Image instructionImage; // Reference to the Image component displaying the instructions
    public TMP_Text stepCountText; // Reference to the TextMeshProUGUI component displaying the step count
    public StepInstructionsSO stepInstructionsSO; // Reference to the StepInstructionsSO scriptable object
    public float fadeDuration = 0.5f; // Duration of the fade animation in seconds

    private int currentStep = 0; // Variable to track the current step index
    public CanvasGroup canvasGroup; // Reference to the CanvasGroup component for fading animation

    private void Start()
    {

        UpdateInstructionText(); // Initial update of the instruction text and image
    }

    private void UpdateInstructionText()
    {
        StartCoroutine(FadeOut(() =>
        {
            instructionText.text = stepInstructionsSO.Instructions[currentStep]; // Update the instruction text with the current step's instruction
            instructionImage.sprite = stepInstructionsSO.InstructionImages[currentStep]; // Update the instruction image with the current step's image
            stepCountText.text = $"{currentStep + 1}/{stepInstructionsSO.Instructions.Length}"; // Update the step count text
            StartCoroutine(FadeIn()); // Start the fade-in coroutine after the fade-out is complete
        }));
    }

    public void MoveForward()
    {
        if (currentStep < stepInstructionsSO.Instructions.Length - 1)
        {
            currentStep++; // Increment the current step index if there are more steps available
            UpdateInstructionText(); // Update the instruction text
        }
    }

    public void MoveBackward()
    {
        if (currentStep > 0)
        {
            currentStep--; // Decrement the current step index if there are previous steps
            UpdateInstructionText(); // Update the instruction text
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
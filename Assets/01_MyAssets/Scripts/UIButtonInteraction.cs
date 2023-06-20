using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIButtonInteraction : MonoBehaviour
{
    public TextFader textFader;
    public TMP_Text[] texts;
    private int currentStep = 0;

    private void NextStep()
    {
        textFader.FadeOut();
        currentStep++;

        if (currentStep >= texts.Length)
        {
            currentStep = 0;
        }

        Invoke("SetNewText", textFader.fadeDuration);
    }

    private void PreviousStep()
    {
        textFader.FadeOut();
        currentStep--;

        if (currentStep < 0)
        {
            currentStep = texts.Length - 1;
        }

        Invoke("SetNewText", textFader.fadeDuration);
    }

    private void SetNewText()
    {
        textFader.SetNewText(texts[currentStep]);
        textFader.FadeIn();
    }
}
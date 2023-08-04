using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePlayer : MonoBehaviour
{
    public float fadeDuration = 1.0f; // Duration of the fade effect in seconds

    public Image blackImage;
    private Color targetColor;

    private void Start()
    {

        StartFadeFromBlack();
    }

    public void StartFadeToBlack()
    {
        targetColor = Color.black;
        StartCoroutine(FadeToColor());
    }

    public void StartFadeFromBlack()
    {
        targetColor = Color.clear;
        StartCoroutine(FadeToColor());
    }

    private IEnumerator FadeToColor()
    {
        float elapsedTime = 0f;
        Color initialColor = blackImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackImage.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }
    }
}

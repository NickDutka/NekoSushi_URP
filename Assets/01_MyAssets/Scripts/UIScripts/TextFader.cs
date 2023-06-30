using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextFader : MonoBehaviour
{
    public float fadeDuration = 2f;

    private TMP_Text text;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void FadeOut()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeText(0f));
    }

    public void FadeIn()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeText(1f));
    }

    public void SetNewText(TMP_Text newText)
    {
        text = newText;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
    }

    private IEnumerator FadeText(float targetAlpha)
    {
        float startAlpha = text.color.a;
        float increment = (targetAlpha - startAlpha) / fadeDuration;

        while (text.color.a != targetAlpha)
        {
            float newAlpha = Mathf.MoveTowards(text.color.a, targetAlpha, increment * Time.deltaTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, newAlpha);

            yield return null;
        }
    }
}
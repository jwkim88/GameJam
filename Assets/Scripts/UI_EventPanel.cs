using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_EventPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descText;
    Animator animator;
    Coroutine showTextCoroutine;
    Color textBaseColor;
    Color textBaseColorTransparent;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        textBaseColor = descText.color;
        textBaseColorTransparent = new Color(textBaseColor.r, textBaseColor.g, textBaseColor.b, 0);
    }

    public void Show(string text, string text2)
    {
        descText.text = text;
        animator.SetBool("Show", true);
        if (showTextCoroutine != null) StopCoroutine(showTextCoroutine);
        showTextCoroutine = StartCoroutine(ShowText(text, text2));
    }

    bool inputReceived = false;
    public void OnInputReceived()
    {
        inputReceived = true;
    }

    IEnumerator ShowText(string text, string text2)
    {
        
        descText.text = text;
        yield return StartCoroutine(TextFadeIn());
        yield return StartCoroutine(TextDisplayDelay());
        yield return StartCoroutine(TextFadeOut());

        descText.text = text2;
        yield return StartCoroutine(TextFadeIn());

        yield return StartCoroutine(TextDisplayDelay());
        yield return StartCoroutine(TextFadeOut());
        Hide();
    }

    IEnumerator TextFadeIn()
    {
        inputReceived = false;
        float time = 0;
        float fadeDuration = 1f;
        float normalizedTime = 0;
        inputReceived = false;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            normalizedTime = time / fadeDuration;
            descText.color = Color.Lerp(textBaseColorTransparent, textBaseColor, normalizedTime);
            yield return null;
        }
    }

    IEnumerator TextFadeOut()
    {
        inputReceived = false;
        float time = 1;
        float fadeDuration = 1f;
        float normalizedTime = 0;
        inputReceived = false;
        while (time > 0 && !inputReceived)
        {
            time -= Time.deltaTime;
            normalizedTime = time / fadeDuration;
            descText.color = Color.Lerp(textBaseColorTransparent, textBaseColor, normalizedTime);
            yield return null;
        }
    }

    IEnumerator TextDisplayDelay()
    {
        inputReceived = false;
        float textDisplayTime = 0f;
        float textDisplayDuration = 5f;
        textDisplayTime = 0;
        while (textDisplayTime < textDisplayDuration && !inputReceived)
        {
            textDisplayTime += Time.deltaTime;
            yield return null;
        }

    }
    public void Hide()
    {
        animator.SetBool("Show", false);
    }
}

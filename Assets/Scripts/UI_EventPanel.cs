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

    public void OnInputReceived()
    {
        if (showTextCoroutine != null) StopCoroutine(showTextCoroutine);
        Hide();
    }

    IEnumerator ShowText(string text, string text2)
    {
        float time = 0;
        float fadeDuration = 1f;
        float normalizedTime = 0;
        descText.text = text;
        while(time < fadeDuration)
        {
            time += Time.deltaTime;
            normalizedTime = time / fadeDuration;
            descText.color = Color.Lerp(textBaseColorTransparent, textBaseColor, normalizedTime);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        while (time > 0)
        {
            time -= Time.deltaTime;
            normalizedTime = time / fadeDuration;
            descText.color = Color.Lerp(textBaseColorTransparent, textBaseColor, normalizedTime);
            yield return null;
        }

        descText.text = text2;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            normalizedTime = time / fadeDuration;
            descText.color = Color.Lerp(textBaseColorTransparent, textBaseColor, normalizedTime);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        while (time > 0)
        {
            time -= Time.deltaTime;
            normalizedTime = time / fadeDuration;
            descText.color = Color.Lerp(textBaseColorTransparent, textBaseColor, normalizedTime);
            yield return null;
        }
        Hide();
    }

    public void Hide()
    {
        animator.SetBool("Show", false);
    }
}

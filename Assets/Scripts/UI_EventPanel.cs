using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_EventPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descText;
    Animator animator;
    Coroutine showTextCoroutine;
    private void Awake()
    {
        animator = GetComponent<Animator>();
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
            descText.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, normalizedTime);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        while (time > 0)
        {
            time -= Time.deltaTime;
            normalizedTime = time / fadeDuration;
            descText.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, normalizedTime);
            yield return null;
        }

        descText.text = text2;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            normalizedTime = time / fadeDuration;
            descText.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, normalizedTime);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        while (time > 0)
        {
            time -= Time.deltaTime;
            normalizedTime = time / fadeDuration;
            descText.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, normalizedTime);
            yield return null;
        }
        Hide();
    }

    public void Hide()
    {
        animator.SetBool("Show", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_EventPanel : MonoBehaviour
{
    [SerializeField] Sprite karmaGain;
    [SerializeField] Sprite karmaLoss;
    [SerializeField] Sprite timePass;
    [SerializeField] Sprite sentToHeaven;
    [SerializeField] Sprite sentToHell;
    [SerializeField] Image icon;
    
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

    public void Show(string text, string text2, Destination destination, GameManager.Outcome outcome)
    {
        descText.text = text;
        animator.SetBool("Show", true);

        Sprite destinationSprite = null;
        switch (destination)
        {
            case Destination.Heaven: destinationSprite = sentToHeaven; break;
            case Destination.Hell: destinationSprite = sentToHell; break;
            case Destination.Purgatory: destinationSprite = timePass; break;
        }

        Sprite outcomeSprite = outcome == GameManager.Outcome.KarmaGain ? karmaGain : karmaLoss;

        if (showTextCoroutine != null) StopCoroutine(showTextCoroutine);
        showTextCoroutine = StartCoroutine(ShowText(text, text2, destinationSprite, outcomeSprite));
    }

    bool inputReceived = false;
    public void OnInputReceived()
    {
        inputReceived = true;
    }

    IEnumerator ShowText(string text, string text2, Sprite sprite1, Sprite sprite2)
    {

        icon.sprite = sprite1;
        icon.SetNativeSize();
        descText.text = text;
        yield return StartCoroutine(TextFadeIn());
        yield return StartCoroutine(TextDisplayDelay());
        yield return StartCoroutine(TextFadeOut());

        icon.sprite = sprite2;
        icon.SetNativeSize();
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
            icon.color = new Color(1, 1, 1, normalizedTime);
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
            icon.color = new Color(1, 1, 1, normalizedTime);
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

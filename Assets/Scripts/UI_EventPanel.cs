using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_EventPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descText;
    Animator animator;
    Coroutine hideCoroutine;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Show(string text)
    {
        descText.text = text;
        animator.SetBool("Show", true);
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    public void OnInputReceived()
    {
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        Hide();
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(10f);
        Hide();
    }

    public void Hide()
    {
        animator.SetBool("Show", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_KarmaCrystal : MonoBehaviour
{
    [SerializeField] private Image crystalFill;
    Coroutine fillCoroutine;
    public void SetFill(float amount)
    {
        if (fillCoroutine != null) StopCoroutine(fillCoroutine);
        fillCoroutine = StartCoroutine(FillOverTime(amount));
    }

    IEnumerator FillOverTime(float amount)
    {
        float duration = 0.5f;
        float time = 0;
        float normalizedTime = 0;
        float startAmount = crystalFill.fillAmount;
        while(time < duration)
        {
            time += Time.deltaTime;
            normalizedTime = time / duration;
            crystalFill.fillAmount = Mathf.Lerp(startAmount, amount, normalizedTime);
            crystalFill.color = Color.Lerp(Color.red, Color.white, crystalFill.fillAmount);
            yield return null;
        }
    }
}

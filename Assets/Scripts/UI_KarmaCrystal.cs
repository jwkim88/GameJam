using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_KarmaCrystal : MonoBehaviour
{
    [SerializeField] private Image crystalFill;

    public void SetFill(float amount)
    {
        crystalFill.fillAmount = amount;
        crystalFill.color = Color.Lerp(Color.red, Color.white, amount);
    }
}

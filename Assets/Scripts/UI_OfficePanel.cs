using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_OfficePanel : MonoBehaviour
{
    [SerializeField] protected GameManager gm;
    [SerializeField] protected TextMeshProUGUI yearCounter;
    [SerializeField] protected TextMeshProUGUI heavenAvailability;
    [SerializeField] protected TextMeshProUGUI hellAvailability;
    [SerializeField] protected TextMeshProUGUI karmicBalance;

    public void UpdateYear(int year)
    {
        this.yearCounter.text = year.ToString();
    }

    public void HeavenAvailability(int availability)
    {
        this.heavenAvailability.text = "Heaven: " + availability;
    }

    public void HellAvailability(int availability)
    {
        this.hellAvailability.text = "Hell: " + availability;
    }

    public void UpdateKarmicBalance(int karmicBalance)
    {
        this.karmicBalance.text = "Karmic Balance: " + karmicBalance;
    }

    public void OnHeavenPressed()
    {
        gm.OnHeavenSelected();
    }

    public void OnHellPressed()
    {
        gm.OnHellSelected();
    }

    public void OnPurgatoryPressed()
    {
        gm.OnPurgatorySelected();
    }
}

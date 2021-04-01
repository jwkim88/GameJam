using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected CharacterManager cm;
    [SerializeField] protected UI_OfficePanel officePanel;
    [SerializeField] protected int year = 2000;
    [SerializeField] protected int yearIncrement = 100;
    [SerializeField] protected int heavenAvailability = 3;
    [SerializeField] protected int hellAvailability = 3;

    CharacterData cd;
    // Start is called before the first frame update

    private void Start()
    {
        cd = cm.characters[0];
        officePanel.UpdateYear(year);
        officePanel.HeavenAvailability(heavenAvailability);
        officePanel.HellAvailability(hellAvailability);
    }

    public void OnTimePass()
    {
        year += 100;
        officePanel.UpdateYear(year);
    }

    public void OnHeavenSelected()
    {
        heavenAvailability--;
        OnTimePass();
        officePanel.HeavenAvailability(heavenAvailability);
    }

    public void OnPurgatorySelected()
    {
        OnTimePass();
        // allow character selection
    }

    public void OnHellSelected()
    {
        hellAvailability--;
        OnTimePass();
        officePanel.HellAvailability(hellAvailability);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected CharacterManager cm;
    [SerializeField] protected UI_OfficePanel officePanel;
    [SerializeField] protected UI_CharacterPanel characterPanel;
    [SerializeField] protected int year = 2000;
    [SerializeField] protected int yearIncrement = 100;
    [SerializeField] protected int heavenAvailability = 3;
    [SerializeField] protected int hellAvailability = 3;
    [SerializeField] protected int karmicBalance = 50;

    CharacterData cd;
    // Start is called before the first frame update

    private void Start()
    {
        officePanel.UpdateYear(year);
        officePanel.HeavenAvailability(heavenAvailability);
        officePanel.HellAvailability(hellAvailability);
        characterPanel.HideCharacter();
    }

    public void OnTimePass()
    {
        year += 100;
        officePanel.UpdateYear(year);
    }

    public void OnHeavenSelected()
    {
        heavenAvailability--;
        OnCharacterInteractionFinished();
    }

    public void OnPurgatorySelected()
    {
        OnCharacterInteractionFinished();
    }

    public void OnHellSelected()
    {
        hellAvailability--;
        OnCharacterInteractionFinished();
    }

    void OnCharacterInteractionFinished()
    {
        characterPanel.HideCharacter();
        officePanel.HeavenAvailability(heavenAvailability);
        officePanel.HellAvailability(hellAvailability);
        OnTimePass();
    }

    public void OnCharacterSelected(CharacterData cd)
    {
        this.cd = cd;
        characterPanel.ShowCharacter(cd);
    }

}

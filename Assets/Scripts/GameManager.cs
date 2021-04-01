using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected CharacterManager cm;
    [SerializeField] protected UI_OfficePanel officePanel;
    [SerializeField] protected UI_CharacterPanel characterPanel;
    [SerializeField] protected UI_WaitingRoomPanel waitingRoomPanel;
    [SerializeField] protected int year = 2000;
    [SerializeField] protected int yearIncrement = 100;
    [SerializeField] protected int heavenAvailability = 3;
    [SerializeField] protected int hellAvailability = 3;
    [SerializeField] protected int karmicBalance = 5;

    CharacterData cd;
    // Start is called before the first frame update

    private void Start()
    {
        cm.Initialize();
        UpdateUI();
        characterPanel.HideCharacter();
    }

    void UpdateUI()
    {
        officePanel.UpdateYear(year);
        officePanel.SetHeavenAvailability(heavenAvailability);
        officePanel.SetHellAvailability(hellAvailability);
        officePanel.UpdateKarmicBalance(karmicBalance);
        officePanel.UpdateButtonInteractability( heavenAvailability > 0,  hellAvailability > 0);
        waitingRoomPanel.UpdateCharacterFadeState();
    }

    public void OnTimePass()
    {
        year += 100;
        cm.IncrementTime(100);
    }

    public void OnHeavenSelected()
    {
        if (cd == null) return;
        heavenAvailability--;
        OnKarmicBalanceChanged(cd, true);
        OnCharacterInteractionFinished();
        waitingRoomPanel.OnCharacterSentToHeaven();
    }

    public void OnPurgatorySelected()
    {
        if (cd == null) return;
        OnCharacterInteractionFinished();
        waitingRoomPanel.OnCharacterReturnedToPurgatory();
    }

    public void OnHellSelected()
    {
        if (cd == null) return;
        hellAvailability--;
        OnKarmicBalanceChanged(cd, false);
        OnCharacterInteractionFinished();
        waitingRoomPanel.OnCharacterSentToHell();
    }

    void OnCharacterInteractionFinished()
    {
        characterPanel.HideCharacter();
        OnTimePass();
        UpdateUI();
    }

    public void OnCharacterSelected(CharacterData cd)
    {
        this.cd = cd;
        characterPanel.ShowCharacter(cd);
    }

    public bool IsCharacterRighteous(CharacterData cd)
    {
        int karma = 0;
        DeedData dd;
        for (int i = 0; i < cd.deeds.Count; i++)
        {
            dd = cm.deeds.Find(d => d.id == cd.deeds[i]);
            karma += dd.karma;
        }
        return karma > 0;
    }

    public void OnKarmicBalanceChanged(CharacterData cd, bool sentToHeaven)
    {
        bool correctChoice = IsCharacterRighteous(cd) == sentToHeaven; 
        DeedData dd;
        int karma = 0;
        int multiplier = correctChoice ? 5 : -5;
        for(int i = 0; i < cd.deeds.Count; i++)
        {
            dd = cm.deeds.Find(d => d.id == cd.deeds[i]);
            karma += dd.karma;
        }

        karmicBalance += karma * multiplier;
        
    }
}

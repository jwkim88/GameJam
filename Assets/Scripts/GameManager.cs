using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected CharacterManager cm;
    [SerializeField] protected UI_OfficePanel officePanel;
    [SerializeField] protected UI_CharacterPanel characterPanel;
    [SerializeField] protected UI_WaitingRoomPanel waitingRoomPanel;
    [SerializeField] protected UI_EventPanel eventPanel;
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
        officePanel.SetHeavenAvailability(heavenAvailability);
        officePanel.SetHellAvailability(hellAvailability);
    }

    void UpdateUI()
    {
        officePanel.UpdateYear(year);

        officePanel.UpdateKarmicBalance(karmicBalance);
        officePanel.UpdateKarmaCrystal((float)karmicBalance / 100);
        officePanel.UpdateButtonInteractability( heavenAvailability > 0,  hellAvailability > 0);
        
        waitingRoomPanel.UpdateCharacterFadeState();
    }

    public void OnTimePass()
    {
        year += 100;
        cm.IncrementTime(100);


        List<CharacterData> fadedSouls = cm.characters.FindAll(c => c.Faded);
        bool soulsHaveFaded = fadedSouls.Count > 0;
        karmicBalance -= fadedSouls.Count * 5;
        waitingRoomPanel.UpdateCharacterFadeState();

        string judgmentText = "";

        string eventText = "Another century passes.";
        string fadedText = "\n\nA few ancient souls have lost all hope, \nfading to dust. \n\nYour failure to judge them upsets the karmic balance.";
        string correctChoiceText = "\n\nYour judgment of " + cd.name + " was just,\nrestoring the karmic balance.";
        string wrongChoiceText = "\n\nYour judgment of " + cd.name + " was unjust,\nupsetting the karmic balance.";
        string endText = "\n\nRestless souls await your judgment, \neager to be released from eternal ennui...";
        
        switch(cd.destination)
        {
            case Destination.Heaven: 
                judgmentText = "You send " + cd.name + " to Heaven,\nto live in bliss for all eternity.";
                judgmentText += cd.correctChoiceMade ? correctChoiceText : wrongChoiceText;
                break;
            case Destination.Hell: 
                judgmentText = "You send " + cd.name + " to Hell,\ndamning them to eternal torment.";
                judgmentText += cd.correctChoiceMade ? correctChoiceText : wrongChoiceText;
                break;
            case Destination.Purgatory: 
                judgmentText = "You keep " + cd.name + " in Purgatory, \nasking them to reflect upon their deeds in life." ; 
                break;
        }

     
        if (soulsHaveFaded) eventText += fadedText;
        else eventText += endText;

        eventPanel.Show(judgmentText, eventText);

        cm.RemoveDepartedCharacters();
        cm.GenerateCharacters();
    }

    public void OnHeavenSelected()
    {
        if (cd == null) return;
        cd.destination = Destination.Heaven;
        heavenAvailability--;
        officePanel.MoveHeavenBead();
        cd.correctChoiceMade = IsCharacterRighteous(cd) == true;
        OnKarmicBalanceChanged(cd, cd.correctChoiceMade);
        OnCharacterInteractionFinished();
        waitingRoomPanel.OnCharacterSentToHeaven();
    }

    public void OnPurgatorySelected()
    {
        if (cd == null) return;
        cd.destination = Destination.Purgatory;
        OnCharacterInteractionFinished();
        waitingRoomPanel.OnCharacterReturnedToPurgatory();
    }

    public void OnHellSelected()
    {
        if (cd == null) return;
        cd.destination = Destination.Hell;
        hellAvailability--;
        officePanel.MoveHellBead();
        cd.correctChoiceMade = IsCharacterRighteous(cd) == false;
        OnKarmicBalanceChanged(cd, cd.correctChoiceMade);
        OnCharacterInteractionFinished();
        waitingRoomPanel.OnCharacterSentToHell();
    }

    void OnCharacterInteractionFinished()
    {
        characterPanel.HideCharacter();
        OnTimePass();
        cd = null;
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

    public void OnKarmicBalanceChanged(CharacterData cd, bool correctChoice)
    {
       
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

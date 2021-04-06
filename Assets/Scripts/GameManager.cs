using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Outcome { KarmaGain, KarmaLoss, Disappear}
    public enum GameState { Selection, Interview, Event}

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
    protected GameState state;

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

    public void OnGameStart()
    {
        SetGameState(GameState.Event);
        eventPanel.Show("After millenia of human history, both Heaven and Hell are quite full.\n\n " +
            "And so most souls wait in Purgatory, milling about listlessly, waiting for their final judgment.",
            "You have been entrusted with the uneviable task of choosing who among them shall leave, and who shall stay.\n\n" +
            "Take care with your judgment, for the karmic balance of the universe rests on your fingertips.", Destination.Purgatory, Outcome.KarmaGain);

    }

    public void SetGameState(GameState state)
    {
        this.state = state;
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

        string fadedText = "\n\nA few ancient souls have lost all hope, \nfading to dust. \n\nYour failure to judge them upsets the karmic balance.";
        string correctChoiceText = "Your judgment of " + cd.name + " was found to be just, restoring the karmic balance.";
        string wrongChoiceText = "Your judgment of " + cd.name + " was found to be unjust, upsetting the karmic balance.";
        string endText = "\n\nAll those who remain await your judgment, so that they may be released from eternal ennui...";
        
        switch(cd.destination)
        {
            case Destination.Heaven: 
                judgmentText = "You sent " + cd.name + " to Heaven, to live in bliss for all eternity.";

                break;
            case Destination.Hell: 
                judgmentText = "You sent " + cd.name + " to Hell, damning them to eternal torment.";

                break;
            case Destination.Purgatory: 
                judgmentText = "You kept " + cd.name + " in Purgatory, asking them to reflect upon their deeds in life." ; 
                break;
        }

        judgmentText += "\n\nAnd so another century passes.";

        string outcomeText = "Your prudent indecision exasperates those who were have waited for nothing.";
        
        if(cd.destination != Destination.Purgatory) outcomeText = cd.correctChoiceMade ? correctChoiceText : wrongChoiceText;

        if (soulsHaveFaded) outcomeText += fadedText;
        else outcomeText += endText;

        Outcome outcome = cd.correctChoiceMade ? Outcome.KarmaGain : Outcome.KarmaLoss;
        
        eventPanel.Show(judgmentText, outcomeText, cd.destination, outcome);

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
        StartCoroutine(WaitForDepartureAnimation(Destination.Heaven));

    }

    public void OnPurgatorySelected()
    {
        if (cd == null) return;
        cd.destination = Destination.Purgatory;
        StartCoroutine(WaitForDepartureAnimation(Destination.Purgatory));

    }

    public void OnHellSelected()
    {
        if (cd == null) return;
        cd.destination = Destination.Hell;
        hellAvailability--;
        officePanel.MoveHellBead();
        cd.correctChoiceMade = IsCharacterRighteous(cd) == false;
        OnKarmicBalanceChanged(cd, cd.correctChoiceMade);

        StartCoroutine(WaitForDepartureAnimation(Destination.Hell));
    }

    void OnCharacterInteractionFinished()
    {
        characterPanel.HideCharacter();
        OnTimePass();
        cd = null;
        UpdateUI();
        Debug.Log("Character interaction finished!");
    }

    public void OnEventFinished()
    {
        SetGameState(GameState.Selection);
    }

    IEnumerator WaitForDepartureAnimation(Destination destination)
    {
        SetGameState(GameState.Selection);
        switch (destination)
        {
            case Destination.Heaven:
                waitingRoomPanel.OnCharacterSentToHeaven();
                yield return new WaitForSeconds(1f);
                break;
            case Destination.Hell:
                waitingRoomPanel.OnCharacterSentToHell();
                yield return new WaitForSeconds(1f);
                break;
            case Destination.Purgatory:
                waitingRoomPanel.OnCharacterReturnedToPurgatory();
                break;
        }
        OnCharacterInteractionFinished();
    }

    public void OnCharacterSelected(CharacterData cd)
    {
        SetGameState(GameState.Interview);
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

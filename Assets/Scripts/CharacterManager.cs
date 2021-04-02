using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterData> characters = new List<CharacterData>();
    public List<DeedData> deeds = new List<DeedData>();
    public List<DeathData> deaths  = new List<DeathData>();
    public List<string> givenNamesMale = new List<string>();
    public List<string> givenNamesFemale = new List<string>();
    public List<string> surnames = new List<string>();
    public List<PortraitData> portraits = new List<PortraitData>();
    [SerializeField] private UI_CharacterPanel characterPanel;
    [SerializeField] private UI_WaitingRoomPanel waitingRoomPanel;
    [SerializeField] private int characterCount;
    public void Initialize()
    {
        GenerateCharacters();
        waitingRoomPanel.Initialize(characters);
    }

    public void RemoveDepartedCharacters()
    {
        for(int i = characters.Count - 1; i >=0; i--)
        {
            if(characters[i].Faded)characters.Remove(characters[i]);
            else if(characters[i].destination != Destination.Purgatory) characters.Remove(characters[i]);
        }
    }

    public void GenerateCharacters()
    {
        for(int i = characters.Count; i < characterCount; i++)
        {
            GenerateCharacter();
        }
    }

    
    private void GenerateCharacter()
    {
        CharacterData cd = new CharacterData();
        cd.gender = Random.Range(0, 2) >= 1 ? Gender.Male : Gender.Female;
        characters.Add(cd);
        AssignRandomPortrait(cd);
        AssignRandomName(cd);
        for(int i = 0; i < 2; i++)
        {
            AssignRandomDeed(cd);
        }
        AssignRandomDeath(cd);
        AssignRandomTime(cd);
        


    }

    private void AssignRandomDeed(CharacterData cd)
    {
        string deedID;

        while (true)
        {
            deedID = deeds[Random.Range(0, deeds.Count - 1)].id;
            if (cd.deeds.Contains(deedID) == false) break;
        }
        cd.deeds.Add(deedID);
    }

    private void AssignRandomDeath(CharacterData cd)
    {
        cd.death = deaths[Random.Range(0, deaths.Count - 1)].id;
    }

    private void AssignRandomName(CharacterData cd)
    {
        List<string> givenNamePool = cd.gender == Gender.Male ? givenNamesMale : givenNamesFemale;
        cd.name = givenNamePool[Random.Range(0, givenNamePool.Count)] + " " + surnames[Random.Range(0, surnames.Count)];
    }

    private void AssignRandomTime(CharacterData cd)
    {
        cd.yearsInPurgatory += Random.Range(0, 700);
    }

    public void IncrementTime(int time)
    {
        for(int i = 0; i < characters.Count; i++)
        {
            characters[i].yearsInPurgatory += time;
            if(characters[i].yearsInPurgatory > 1000)
            {
                Debug.Log(characters[i].name + " is fading!");
            }
        }
    }

    private void AssignRandomPortrait(CharacterData cd)
    {
        PortraitData pd = portraits[Random.Range(0, portraits.Count - 1)];
        cd.sprite = pd.portraitSprite;
        cd.waitingRoomSprite = pd.waitingRoomSprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterData> characters = new List<CharacterData>();
    public List<DeedData> deeds = new List<DeedData>();
    public List<DeedData> deaths  = new List<DeedData>();
    [SerializeField] private UI_CharacterPanel characterPanel;
    [SerializeField] private UI_WaitingRoomPanel waitingRoomPanel;
    [SerializeField] private int characterCount;
    private void Start()
    {
        GenerateCharacters();
        waitingRoomPanel.Initialize(characters);
    }

    private void GenerateCharacters()
    {
        for(int i = 0; i < characterCount; i++)
        {
            GenerateCharacter();
        }
    }

    private void GenerateCharacter()
    {
        CharacterData cd = new CharacterData();
        characters.Add(cd);
        for(int i = 0; i < 2; i++)
        {
            AssignRandomDeed(cd);
        }
        AssignRandomDeath(cd);


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
}

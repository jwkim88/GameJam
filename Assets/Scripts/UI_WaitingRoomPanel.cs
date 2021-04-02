using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WaitingRoomPanel : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] UI_WaitingCharacter characterPrefab;
    [SerializeField] RectTransform characterParent;
    [SerializeField] List<RectTransform> waitingRoomSlots = new List<RectTransform>();
    [SerializeField] RectTransform judgmentSlot;
    List<UI_WaitingCharacter> characters = new List<UI_WaitingCharacter>();
    UI_WaitingCharacter activeCharacter;
    public void Initialize(List<CharacterData> characters)
    {
        for(int i = 0; i < characters.Count; i++)
        {
            AssignCharacter(characters[i]);
        }
    }

    public UI_WaitingCharacter SpawnCharacter()
    {
        return Instantiate(characterPrefab, characterParent); ;
    }

    public void AssignCharacter(CharacterData cd)
    {
        UI_WaitingCharacter c = SpawnCharacter();
        c.Initialize(this);
        
        characters.Add(c);
        for(int i = 0; i < waitingRoomSlots.Count; i++)
        {
            if(waitingRoomSlots[i].childCount == 0)
            {
                c.SetParent(waitingRoomSlots[i]);
                c.AssignCharacter(cd, i);
                break;
            }
        }
        
    }

    public void UpdateCharacterFadeState()
    {
        for(int i = 0; i < characters.Count; i++)
        {
            characters[i].UpdateFade();
            characters[i].UpdateTimeText();
        }
    }

    public void OnCharacterSelected(UI_WaitingCharacter waitingCharacter, CharacterData cd)
    {
        if (activeCharacter != null) return;
        activeCharacter = waitingCharacter;
        activeCharacter.GetComponent<RectTransform>().SetParent(judgmentSlot);
        activeCharacter.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        gm.OnCharacterSelected(cd);
    }

    public void OnCharacterSentToHeaven()
    {
        if (activeCharacter == null) return;
        activeCharacter.gameObject.SetActive(false);
        activeCharacter = null;
    }

    public void OnCharacterSentToHell()
    {
        if (activeCharacter == null) return;
        activeCharacter.gameObject.SetActive(false);
        activeCharacter = null;
    }

    public void OnCharacterReturnedToPurgatory()
    {
        if (activeCharacter == null) return;
        activeCharacter.SetParent(waitingRoomSlots[activeCharacter.slotIndex]);
        activeCharacter = null;
    }

    
}

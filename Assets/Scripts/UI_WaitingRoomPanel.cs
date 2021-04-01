using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WaitingRoomPanel : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] UI_WaitingCharacter characterPrefab;
    [SerializeField] RectTransform characterParent;
    [SerializeField] List<RectTransform> waitingRoomSlots = new List<RectTransform>();
    List<UI_WaitingCharacter> characters = new List<UI_WaitingCharacter>();
    
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
        c.AssignCharacter(cd);
        characters.Add(c);
        for(int i = 0; i < waitingRoomSlots.Count; i++)
        {
            if(waitingRoomSlots[i].childCount == 0)
            {
                c.GetComponent<RectTransform>().SetParent(waitingRoomSlots[i]);
                c.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                break;
            }
        }
    }

    public void OnCharacterSelected(CharacterData cd)
    {
        gm.OnCharacterSelected(cd);
    }

    
}

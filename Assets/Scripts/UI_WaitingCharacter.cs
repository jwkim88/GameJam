using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_WaitingCharacter : MonoBehaviour, IPointerClickHandler
{
    private UI_WaitingRoomPanel panel;
    private CharacterData cd;

    public void Initialize(UI_WaitingRoomPanel panel)
    {
        this.panel = panel;
    }
    public void AssignCharacter(CharacterData cd)
    {
        this.cd = cd;
    }
    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Character Selected: " + cd.name);
        panel.OnCharacterSelected(cd);
    }
}

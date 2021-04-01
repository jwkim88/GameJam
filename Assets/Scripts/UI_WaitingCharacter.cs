using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_WaitingCharacter : MonoBehaviour, IPointerClickHandler
{
    private CharacterData cd;

    public void AssignCharacter(CharacterData cd)
    {
        this.cd = cd;
    }
    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Character Selected: " + cd.name);
    }
}

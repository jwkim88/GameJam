using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterData> characters = new List<CharacterData>();
    [SerializeField] private UI_CharacterPanel characterPanel;
    [SerializeField] private UI_WaitingRoomPanel waitingRoomPanel;
    private void Start()
    {
        waitingRoomPanel.Initialize(characters);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterData> characters = new List<CharacterData>();
    public UI_CharacterPanel characterPanel;
    private void Start()
    {
        characterPanel.ShowCharacter(characters[0]);
    }
}

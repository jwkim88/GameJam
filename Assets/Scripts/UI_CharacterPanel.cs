using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterPanel : MonoBehaviour
{
    public Image portrait;
    public void ShowCharacter(CharacterData cd)
    {
        portrait.sprite = cd.sprite;
        portrait.SetNativeSize();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData 
{
    public string name;
    public int yearsRemaining;
    public List<string> deeds = new List<string>();
    public List<string> sins = new List<string>();
    public Sprite sprite;
}

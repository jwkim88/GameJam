using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData 
{
    public string name;
    public Gender gender;
    public int yearsRemaining;
    public string death;
    public List<string> deeds = new List<string>();
    public Sprite sprite;
}

public enum Gender { Male, Female}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData 
{
    public string name;
    public Gender gender;
    public int yearsInPurgatory;
    public string death;
    public List<string> deeds = new List<string>();
    public Sprite sprite;
    public Sprite waitingRoomSprite;

    public bool Faded
    {
        get { return yearsInPurgatory > 1000; }
    }
}

public enum Gender { Male, Female}
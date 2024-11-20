using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character
{
    public int id;
    public string name;
    public int price;
    public Sprite icon;
    public bool isUnlocked;
    public GameObject model;

    public Character(Character character) 
    {
        id = character.id;
        name = character.name;
        price = character.price;
        icon = character.icon;
        isUnlocked = character.isUnlocked;
        model = character.model;
    }

    public Character(int id, string name, int price, Sprite icon, bool isUnlocked, GameObject modelReview)
    {
        this.id = id;
        this.name = name;
        this.price = price;
        this.icon = icon;
        this.isUnlocked = isUnlocked;
        this.model = modelReview;
    }
}

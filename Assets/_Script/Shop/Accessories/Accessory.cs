using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Accessory
{
    public int id;
    public string name;
    public string description;
    public bool isUnlocked;
    public int price;
    public Sprite icon;

    public Accessory(Accessory accessory)
    {
        id = accessory.id;
        name = accessory.name;
        description = accessory.description;
        isUnlocked = accessory.isUnlocked;
        price = accessory.price;
        icon = accessory.icon;
    }

    public Accessory(int id, string name, string description, bool isUnlocked, int price, Sprite icon)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.isUnlocked = isUnlocked;
        this.price = price;
        this.icon = icon;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
abstract class PowerItem : MonoBehaviour
{
    public int id;
    public string name;
    public int level;
    public bool isActive;

    public PowerItem(int id, string name, string description, int level, bool isActive)
    {
        this.id = id;
        this.name = name;    
        this.level = level;
        this.isActive = isActive;   
    }

    public PowerItem()
    {
        id = -1;
        name = string.Empty;
        level = 1;
        isActive = false;
    }

    public abstract string GetDesByCurrentLv();
    public abstract string GetDesByNextLv();
}

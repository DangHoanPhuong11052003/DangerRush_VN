using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

[Serializable]
public class Power
{
    [SerializeField] public int id;
    [SerializeField] public string name;
    [SerializeField] public string description;
    [SerializeField] public Sprite icon;
    [SerializeField] public List<int> lst_priceByLevel=new List<int>();
    [SerializeField] public int level;
    [SerializeField] public int maxLevel;
    [SerializeField] public List<float> lst_timeActiveByLevel=new List<float>();
    [SerializeField] public bool isActive;

    public Power()
    {
        id = -1;
        name = null;
        description = null;
        icon = null;
        lst_priceByLevel.Clear();
        level = 1;
        maxLevel = 1;
        lst_timeActiveByLevel.Clear();
        isActive = false;
    }
    public Power(int id, string name, string description, Sprite icon, List<int> lst_priceByLevel, int level, int maxLevel, List<float> lst_timeActiveByLevel, bool isActive)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.lst_priceByLevel = lst_priceByLevel;
        this.level = level;
        this.maxLevel = maxLevel;
        this.lst_timeActiveByLevel = lst_timeActiveByLevel;
        this.isActive = isActive;
    }
    public Power(Power power)
    {
        this.id = power.id;
        this.name = power.name;
        this.description = power.description;
        this.icon = power.icon;
        this.lst_priceByLevel = power.lst_priceByLevel;
        this.level = power.level;
        this.maxLevel = power.maxLevel;
        this.lst_timeActiveByLevel=power.lst_timeActiveByLevel;
        this.isActive = power.isActive;
    }
}

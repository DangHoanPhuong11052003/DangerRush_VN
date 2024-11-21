using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;


public abstract class PowerItem : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public string name;
    [SerializeField] public string description;
    [SerializeField] public int price;
    [SerializeField] public int level;
    [SerializeField] public bool isActive;

    private PowerStoreManager powerStoreManager;

    public PowerItem(int id, string name, string description, int level, bool isActive)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.level = level;
        this.isActive = isActive;   
    }

    public PowerItem()
    {
        id = -1;
        name = string.Empty;
        description = string.Empty;
        level = 1;
        isActive = false;
    }

    public void SetData(PowerData powerData, PowerStoreManager powerStoreManager)
    {
        this.powerStoreManager = powerStoreManager;

        this.level=powerData.level;

    }

    public void ShowInfor()
    {
        powerStoreManager.ShowInfor(this);
    }

    public abstract int GetPrice();

    public abstract bool UpgradePower();

    public abstract string GetDesByCurrentLv(int level);
    public string GetDesByNextLv(int level)
    {
        return GetDesByCurrentLv(level+1);
    }
}

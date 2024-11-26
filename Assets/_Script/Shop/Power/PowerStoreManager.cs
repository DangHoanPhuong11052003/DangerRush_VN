using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;


public class PowerStoreManager : MonoBehaviour
{
    [SerializeField] private PowerAssetData PowerAssetData;
    [SerializeField] private Transform itemsTranform;
    [SerializeField] protected GameObject PowerItemPerfab;

    private List<PowerUIItem> lst_powersUIItem= new List<PowerUIItem>();
    private List<PowerData> lst_powersLocalData= new List<PowerData>();
    private List<Power> lst_powerData= new List<Power>();

    private void Start()
    {
        //LocalData.instance.SetCoin(5000);

        UpdateData();
        UpdateItemsUI();
    }

    public void UpdateData()
    {
        lst_powerData.Clear();
        lst_powersLocalData = LocalData.instance.GetPowersData();

        foreach (var item in PowerAssetData.lst_power)
        {
            PowerData powerData = lst_powersLocalData.Find(x => x.id == item.id);
            Power power=new Power();

            if (powerData != null)
            {
                power = new Power(item);
                power.level = powerData.level;
            }
            else
            {
                power = new Power(item);
            }
            lst_powerData.Add(power);
        }
    }

    public void UpdateItemsUI()
    {
        if(lst_powersUIItem.Count>0)
        {
            int i = 0;
            foreach (var item in lst_powersUIItem)
            {
                item.SetData(lst_powerData[i],this);
                i++;
            }
        }
        else
        {
            foreach (var item in lst_powerData)
            {
                GameObject newItem = Instantiate(PowerItemPerfab, itemsTranform);
                PowerUIItem powerUIItem = newItem.GetComponent<PowerUIItem>();
                powerUIItem.SetData(item, this);
                lst_powersUIItem.Add(powerUIItem);
            }
        }
        
    }

    public void UpgradePower(Power powerItem)
    {
        int coin = LocalData.instance.GetCoin();

        //not enough fishbone
        if(coin < powerItem.lst_priceByLevel[powerItem.level])
        {
            EventManager.NotificationToActions(KeysEvent.NotEnoughFishbone.ToString(), powerItem.lst_priceByLevel[powerItem.level] - coin);
            return;
        }

        if (powerItem != null && powerItem.level != powerItem.maxLevel)
        {
            PowerData powerData = lst_powersLocalData.Find(x => x.id == powerItem.id);

            coin -= powerItem.lst_priceByLevel[powerItem.level];
            powerItem.level++;

            if (powerData != null)
            {
                powerData.level = powerItem.level;
            }
            else
            {
                powerData=new PowerData(powerItem.id,powerItem.level);
                lst_powersLocalData.Add(powerData);
            }
            LocalData.instance.SetPowerData(lst_powersLocalData);
            LocalData.instance.SetCoin(coin);

            UpdateItemsUI();
        }
    }
}

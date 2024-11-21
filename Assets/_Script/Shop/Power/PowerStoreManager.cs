using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;


public class PowerStoreManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> lst_powerData= new List<GameObject>();
    [SerializeField] private Transform itemsTranform;

    [SerializeField] private TextMeshProUGUI namePower;
    [SerializeField] private TextMeshProUGUI des;
    [SerializeField] private TextMeshProUGUI inforLevel;
    [SerializeField] private TextMeshProUGUI inforNextLevel;
    [SerializeField] private GameObject buttonUpgrade;
    [SerializeField] private Transform ImgSeleted;

    private List<GameObject> lst_powers= new List<GameObject>();
    private List<PowerData> powersLocalData= new List<PowerData>();
    private PowerItem currentItem;

    private void Start()
    {
        LocalData.instance.SetCoin(1000000);

        powersLocalData = LocalData.instance.GetPowersData();

        foreach (var item in lst_powerData)
        {
            PowerItem powerItem=item.GetComponent<PowerItem>();
            PowerData powerData= powersLocalData.Find(x=>x.id==powerItem.id);

            GameObject gameObject= Instantiate(item,itemsTranform);
            if(powerData!=null)
            {
                gameObject.GetComponent<PowerItem>().SetData(powerData, this);
            }
            else
            {
                gameObject.GetComponent<PowerItem>().SetData(new PowerData(powerItem.id,powerItem.level), this);
            }
            lst_powers.Add(gameObject);
        }

        currentItem = lst_powers[0].GetComponent<PowerItem>();
        ShowInfor(currentItem);
    }


    public void ShowInfor(PowerItem data)
    {
        currentItem = data;

        des.text = data.description;
        inforLevel.text = data.GetDesByCurrentLv(data.level);
        namePower.text = data.name;

        if (data.GetDesByNextLv(data.level) == null)
        {
            inforNextLevel.text = "MAXIMUM LEVEL REACHED";
            buttonUpgrade.SetActive(false);
        }
        else
        {
            inforNextLevel.text = data.GetDesByNextLv(data.level);
            buttonUpgrade.GetComponentInChildren<TextMeshProUGUI>().text = data.GetPrice().ToString();
            buttonUpgrade.SetActive(true);
        }

        ImgSeleted.SetParent(currentItem.transform);
        ImgSeleted.transform.position = currentItem.transform.position;
    }

    public void UpgradePower()
    {
        int coin = LocalData.instance.GetCoin();

        if (currentItem != null&& coin>=currentItem.GetPrice())
        {
            bool isSuccec= currentItem.UpgradePower();

            if (!isSuccec)
            {
                return;
                throw new Exception();
            }

            PowerData powerData= powersLocalData.Find(item => item.id == currentItem.id);
            if (powerData != null)
            {
                powerData.level = currentItem.level;
            }
            else
            {
                powerData=new PowerData(currentItem.id,currentItem.level);
                powersLocalData.Add(powerData);
            }
            LocalData.instance.SetPowerData(powersLocalData);

            coin -= currentItem.GetPrice();
            LocalData.instance.SetCoin(coin);

            ShowInfor(currentItem); 
        }
    }


}

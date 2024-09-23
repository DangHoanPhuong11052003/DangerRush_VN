using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


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
        powersLocalData = LocalData.instance.GetPowersData();

        if (powersLocalData.Count <= 0)
        {
            foreach (var item in lst_powerData)
            {
                PowerItem data = item.GetComponent<PowerItem>();

                powersLocalData.Add(new PowerData(data.id, data.level));
            }

            LocalData.instance.SetPowerData(powersLocalData);
            return;
        }

        foreach (var item in lst_powerData)
        {
            GameObject gameObject= Instantiate(item,itemsTranform);
            gameObject.GetComponent<PowerItem>().SetData(powersLocalData,this);
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

        ImgSeleted.transform.parent = currentItem.transform;
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

            powersLocalData.Find(item => item.id == currentItem.id).level = currentItem.level;
            LocalData.instance.SetPowerData(powersLocalData);

            coin -= currentItem.GetPrice();
            LocalData.instance.SetCoin(coin);

            ShowInfor(currentItem); 
        }
    }


}

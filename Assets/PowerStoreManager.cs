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

    private List<GameObject> lst_powers= new List<GameObject>();
    private List<PowerData> powersLocalData= new List<PowerData>();
    private GameObject currentItem;

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

        currentItem = lst_powers[0];
        ShowInfor(currentItem.GetComponent<PowerItem>());

    }


    public void ShowInfor(PowerItem data)
    {
        des.text = data.description;
        inforLevel.text = data.GetDesByCurrentLv(data.level);
        inforNextLevel.text = data.GetDesByNextLv(data.level);
        namePower.text = data.name;
    }
}

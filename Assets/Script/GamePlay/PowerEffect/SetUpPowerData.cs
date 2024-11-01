using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SetUpPowerData : MonoBehaviour
{
    [SerializeField] private List<PowerEffect> powerList=new List<PowerEffect>();

    void Start()
    {
        List<PowerData> powersData = LocalData.instance.GetPowersData();

        foreach (var item in powersData)
        {
            int index = powerList.FindIndex(i => i.id == item.id);
            if(index != -1)
            {
                powerList[index].SetData(item);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class StoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fishBoneValue;

    private void Update()
    {
        fishBoneValue.text = LocalData.instance.GetCoin().ToString().Length < 9 ? LocalData.instance.GetCoin().ToString() : LocalData.instance.GetCoin().ToString().Substring(10) + "...";
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class StoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fishBoneValue;

    private GameData gameData;

    private void Start()
    {
        gameData = LocalData.instance.GetGameData();

        fishBoneValue.text = gameData.coin.ToString().Length<9? gameData.coin.ToString(): gameData.coin.ToString().Substring(10)+"...";
    }


}

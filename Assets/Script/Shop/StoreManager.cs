using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class CharacterItem
{
    public int id;
    public string name;
    public Sprite icon;
    public GameObject characterPf;
    public bool isUnlocked;
}

public class StoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fishBoneValue;

    [SerializeField] private List<CharacterItem> characterItem=new List<CharacterItem>();

    private GameData gameData;

    private void Start()
    {
        gameData = LocalData.instance.GetGameData();

        fishBoneValue.text = gameData.coin.ToString().Length<9? gameData.coin.ToString(): gameData.coin.ToString().Substring(10)+"...";
    }


}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fishBoneValue;

    private void OnEnable()
    {
        EventManager.Subscrice(KeysEvent.CoinUpdate.ToString(),UpdateCoinValue);
        fishBoneValue.text = LocalData.instance.GetCoin().ToString();
    }

    private void OnDisable()
    {
        EventManager.UnSubscrice(KeysEvent.CoinUpdate.ToString(), UpdateCoinValue);
    }

    private void UpdateCoinValue(object parameter)
    {
        fishBoneValue.text = parameter.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fishBoneValue;
    [SerializeField] private float timeToUpdateCoin = 1.5f;
    private int coinData;
    private int currentCoinShow;

    private void OnEnable()
    {
        EventManager.Subscrice(KeysEvent.CoinUpdate.ToString(),UpdateCoinValue);
        coinData= LocalData.instance.GetCoin();
        currentCoinShow= coinData;
        fishBoneValue.text = currentCoinShow.ToString();
    }

    private void OnDisable()
    {
        EventManager.UnSubscrice(KeysEvent.CoinUpdate.ToString(), UpdateCoinValue);
    }

    private void UpdateCoinValue(object parameter)
    {
        coinData = (int)parameter;
        if (coinData != currentCoinShow)
            StartCoroutine(AnimationCoinUpdate(currentCoinShow));  
    }

    private IEnumerator AnimationCoinUpdate(float startValue)
    {
        float timer = 0f;
        while (timer < timeToUpdateCoin)
        {
            timer += Time.unscaledDeltaTime;
            int newValue=Mathf.RoundToInt(Mathf.Lerp(startValue, coinData, timer/timeToUpdateCoin));
            currentCoinShow = coinData;
            fishBoneValue.text=newValue.ToString();
            yield return null;
        }
        currentCoinShow = coinData;
        fishBoneValue.text=coinData.ToString();
    }
}

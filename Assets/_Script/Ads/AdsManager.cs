using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
    [SerializeField] protected RewardedAdsButton RewardedAdsButton;
    [SerializeField] protected GameObject NotEnoughFishboneUI;
    [SerializeField] protected Button ButtonAds;
    [SerializeField] protected int quantityFishbonesReward;

    public static AdsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        EventManager.Subscrice(KeysEvent.NotEnoughFishbone.ToString(), ShowNotEnoughFishboneUI);

        AddButtonAdsReward(ButtonAds, GetFishboneAfterAds);
    }

    private void OnDisable()
    {
        EventManager.UnSubscrice(KeysEvent.NotEnoughFishbone.ToString(), ShowNotEnoughFishboneUI);
        RemoveButtonAdsReward(ButtonAds);
    }

    public void AddButtonAdsReward(Button button,Action<bool> func)
    {
        RewardedAdsButton.AddButtonAds(button, func);
        RewardedAdsButton.LoadAd();
    }

    public void RemoveButtonAdsReward(Button button)
    {
        RewardedAdsButton.RemoveButtonAds(button);
    }

    private void ShowNotEnoughFishboneUI(object parameter)
    {
        if(NotEnoughFishboneUI==null&&ButtonAds==null) 
        {
            Destroy(this);
            return; 
        }

        RewardedAdsButton.LoadAd();
        NotEnoughFishboneUI.SetActive(true);
    }

    public void CloseNotEnoughFishboneUI()
    {
        if (NotEnoughFishboneUI == null && ButtonAds == null)
        {
            Destroy(this);
            return;
        }
        NotEnoughFishboneUI.SetActive(false);
    }

    private void GetFishboneAfterAds(bool isComplete)
    {
        if (isComplete)
        {
            int coin = LocalData.instance.GetCoin();
            LocalData.instance.SetCoin(coin + quantityFishbonesReward);
        }
        else
        {
            //show check your internet UI
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.AudioSettings;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private RewardedAdsButton RewardedAdsButton;
    [SerializeField] private GameObject NotEnoughFishboneUI;
    [SerializeField] private Button ButtonAds;
    [SerializeField] private int quantityFishbonesReward;
    [SerializeField] private GameObject NoInternetUI;

    public static AdsManager instance;

    private void Awake()
    {
#if UNITY_STANDALONE_WIN
        Destroy(gameObject);
        return;
#endif


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
        if(NotEnoughFishboneUI==null||ButtonAds==null||NoInternetUI==null) 
        {
            Destroy(this);
            return; 
        }

        RewardedAdsButton.LoadAd();
        NotEnoughFishboneUI.SetActive(true);
    }

    public void OpenOrCloseNoInternetUI(bool isOpen)
    {
        if (NotEnoughFishboneUI == null || ButtonAds == null || NoInternetUI == null)
        {
            Destroy(this);
            return;
        }

        NoInternetUI.SetActive(isOpen);
        if (isOpen)
        {
            NotEnoughFishboneUI.SetActive(false);
        }
    }

    public void CloseNotEnoughFishboneUI()
    {
        if (NotEnoughFishboneUI == null || ButtonAds == null || NoInternetUI == null)
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

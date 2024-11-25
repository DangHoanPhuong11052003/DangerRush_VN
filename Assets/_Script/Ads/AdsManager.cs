using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private RewardedAdsButton RewardedAdsButton;
    [SerializeField] private GameObject NotEnoughFishboneUI;
    [SerializeField] private Button ButtonAds;
    [SerializeField] private int quantityFishbonesReward;
    public static AdsManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        EventManager.Subscrice(KeysEvent.NotEnoughFishbone.ToString(), ShowNotEnoughFishboneUI);
        AddButtonAdsReward(ButtonAds);
    }

    private void OnDisable()
    {
        EventManager.UnSubscrice(KeysEvent.NotEnoughFishbone.ToString(), ShowNotEnoughFishboneUI);
        RemoveButtonAdsReward(ButtonAds);
    }

    public void AddButtonAdsReward(Button button)
    {
        RewardedAdsButton.AddButtonAds(button, GetFishboneAfterAds);
        RewardedAdsButton.LoadAd();
    }

    public void RemoveButtonAdsReward(Button button)
    {
        RewardedAdsButton.RemoveButtonAds(button);
    }

    private void ShowNotEnoughFishboneUI(object parameter)
    {
        NotEnoughFishboneUI.SetActive(true);
    }

    public void CloseNotEnoughFishboneUI()
    {
        NotEnoughFishboneUI.SetActive(false);
    }

    private void GetFishboneAfterAds()
    {
        int coin=LocalData.instance.GetCoin();
        LocalData.instance.SetCoin(coin+quantityFishbonesReward);
    }
}

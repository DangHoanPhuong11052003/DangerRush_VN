using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections.Generic;
using System;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Dictionary<Button,Action> dic_showAdButton=new Dictionary<Button, Action>();
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms
    Action _CurrentFuncGetReward;

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // Disable the button until the ad is ready to show:
        if (dic_showAdButton.Count > 0)
        {
            foreach (var item in dic_showAdButton)
            {
                item.Key.interactable = false;
            }
        }
    }

    // Call this public method when you want to get an ad ready to show.
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            if (dic_showAdButton.Count > 0)
            {
                foreach (var button in dic_showAdButton)
                {
                    button.Key.onClick.AddListener(()=>ShowAd(button.Key));
                    // Enable the button for users to click:
                    button.Key.interactable = true;
                }
            }
        }
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd(Button button)
    {
        // Disable the button:
        if (dic_showAdButton.Count > 0)
        {
            foreach (var item in dic_showAdButton)
            {
                item.Key.interactable = false;
                if (item.Key == button)
                {
                    _CurrentFuncGetReward=(item.Value);
                }
            }
        }
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");

            _CurrentFuncGetReward?.Invoke();

            AdsManager.instance.CloseNotEnoughFishboneUI();
            LoadAd();
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Clean up the button listeners:
        if (dic_showAdButton.Count > 0)
        {
            foreach (var button in dic_showAdButton)
            {
                button.Key.onClick.RemoveAllListeners();
            }
        }
    }

    public void AddButtonAds(Button button,Action GetRewardFunction)
    {
        if (button != null)
        {
            dic_showAdButton.Add(button, GetRewardFunction);
        }
    }

    public void RemoveButtonAds(Button button)
    {
        if (button != null)
        {
            dic_showAdButton.Remove(button);
        }
    }
}
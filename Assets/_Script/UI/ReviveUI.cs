using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreValueTxt;
    [SerializeField] private TextMeshProUGUI TimerValueTxt;
    [SerializeField] private Slider TimerSlider;
    [SerializeField] private int timeCount;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private UIGamePlayManager gamePlayManager;
    [SerializeField] private Button ButtonWatchAd;

    private float timer=0;
    private bool isWatchedAd=false;

    private void OnEnable()
    {
#if UNITY_STANDALONE_WIN
        Destroy(gameObject);
        return;
#endif

        if (isWatchedAd)
        {
            CloseUI();
        }
        timer = timeCount;
        TimerSlider.maxValue = timer;
        TimerSlider.value = timer;
        ScoreValueTxt.text = Mathf.CeilToInt(playerManager.score).ToString();
    }
    private void Start()
    {
        AdsManager.instance.AddButtonAdsReward(ButtonWatchAd, WatchedAds);
    }
    private void OnDestroy()
    {
        AdsManager.instance.RemoveButtonAdsReward(ButtonWatchAd);

    }
    private void Update()
    {
        timer -= Time.unscaledDeltaTime;
        TimerSlider.value = timer;
        TimerValueTxt.text=Mathf.FloorToInt(timer).ToString();

        if (timer <= 0)
        {
            CloseUI();
        }
    }

    public void CloseUI()
    {
        gamePlayManager.OpenOrCloseReviveUI(false);
        playerManager.LoseGamePublic();
    }

    private void WatchedAds(bool isComplete)
    {
        if(isComplete)
        {
            isWatchedAd = true;
            playerManager.QuantityLife += 1;
            playerManager.StunPublic();
            gamePlayManager.OpenOrCloseReviveUI(false);
        }
    }
}

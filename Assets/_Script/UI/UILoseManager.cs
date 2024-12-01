using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UILoseManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI fishBoneValue;
    [SerializeField] private TextMeshProUGUI fishBoneBonusValue;

    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private Achiverments achiverments_FirstDead;
    [SerializeField] private Button ButtonAd;

    private int coinReward = 0;

    private bool isPCPlatform=false;

    private void OnEnable()
    {
#if UNITY_STANDALONE_WIN
        isPCPlatform = true;
#endif
        coinReward = Mathf.CeilToInt(playerManager.coin * 0.5f);

        score.text =Mathf.CeilToInt(playerManager.score) +"M";
        fishBoneValue.text= playerManager.coin.ToString();
        fishBoneBonusValue.text = "+"+ coinReward;

        Canvas.ForceUpdateCanvases();

        //check achiement when player lose
        AchievementsManager.instance.UnlockAchievement(achiverments_FirstDead.ToString());
        AchievementsManager.instance.CheckScoreAchievement(Mathf.CeilToInt(playerManager.score));

        //check daily quest when player lose
        DailyQuestManager.instance.UpdateHighStageQuest( Mathf.CeilToInt(playerManager.score), DailyQuestsType.getScore);
    }
    private void Start()
    {
        if (!isPCPlatform)
        {
            AdsManager.instance.AddButtonAdsReward(ButtonAd, RewardAds);
        }
        else
        {
            ButtonAd.gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        if(!isPCPlatform)
        {
            AdsManager.instance.RemoveButtonAdsReward(ButtonAd);
        }
    }

    private void RewardAds(bool isComplete)
    {
        if(isComplete)
        {
            int coin= LocalData.instance.GetCoin();
            LocalData.instance.SetCoin(coin+ coinReward);
        }
    }
}

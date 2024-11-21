using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementItemUI : MonoBehaviour
{
    [SerializeField] private Sprite UnlockBgImage;
    [SerializeField] private Sprite LockBgImage;
    [SerializeField] private Color UnlockCupColor;
    [SerializeField] private Color LockCupColor;
    [SerializeField] private Image BgUI;
    [SerializeField] private Image CupUI;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI des;
    [SerializeField] private TextMeshProUGUI rewardValue;

    [SerializeField] private GameObject RewardUI;

    private Achievement achievement;
    private AchivemenUIManager AchivemenUIManager;

    public void SetData(Achievement value,AchivemenUIManager achivemenUIManager)
    {
        achievement = value;
        this.AchivemenUIManager = achivemenUIManager;

        if (achievement.isUnlocked)
        {
            BgUI.sprite = UnlockBgImage;
            CupUI.color = UnlockCupColor;
            title.text = achievement.title;
            des.text = achievement.description;

            RewardUI.SetActive(!achievement.isGotReward);
            rewardValue.text = achievement.rewardValue.ToString();
        }
        else
        {
            BgUI.sprite = LockBgImage;
            CupUI.color = LockCupColor;
            title.text = achievement.title;
            des.text = achievement.description;

            RewardUI.SetActive(false);
        }
    }

    public void GetReward()
    {
        AchivemenUIManager.GetReward(achievement);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AchivemenUIManager : MonoBehaviour
{
    [SerializeField] private AchivementData achievementData;
    [SerializeField] private GameObject achievementItemUIPrefap;
    [SerializeField] private Transform itemsGeneralTranform;
    [SerializeField] private Transform itemsRecordTranform;
    [SerializeField] private Transform itemsOtherTranform;

    private List<AchievementLocalData> achievementLocalDataLst=new List<AchievementLocalData>();

    private List<Achievement> achievementsGeneralLst = new List<Achievement>();
    private List<Achievement> achievementsRecordLst = new List<Achievement>();
    private List<Achievement> achievementsOtherLst = new List<Achievement>();

    private List<AchievementItemUI> achievementItemGeneralUILst=new List<AchievementItemUI>();
    private List<AchievementItemUI> achievementItemRecordUILst = new List<AchievementItemUI>();
    private List<AchievementItemUI> achievementItemOtherUILst = new List<AchievementItemUI>();

    private void OnEnable()
    {
        achievementLocalDataLst = LocalData.instance.GetAchievementLocalData();

        UpdateDataAchivementLst();

        UpdateItemGeneralData();
        UpdateItemRecordData();
        UpdateItemOtherData();
    }

    private void UpdateDataAchivementLst()
    {
        achievementsGeneralLst.Clear();

        foreach (var item in achievementData.achievementsData)
        {
            AchievementLocalData data = achievementLocalDataLst.Find(x => x.id == item.id);

            if (data != null)
            {
                Achievement achievement = new Achievement(item);
                achievement.isUnlocked = true;
                achievement.isGotReward = data.isGotReward;
                achievementsGeneralLst.Add(achievement);
            }
            else
            {
                achievementsGeneralLst.Add(new Achievement(item));
            }
        };
        //sort list by isUnlock=true and isGotReward=true first
        achievementsGeneralLst.Sort((a, b) =>
        {
            if (!a.isGotReward && !b.isGotReward)
            {
                return b.isUnlocked.CompareTo(a.isUnlocked);
            }
            else
            {
                return a.isGotReward.CompareTo(b.isGotReward);
            }
        });

        achievementsRecordLst = achievementsGeneralLst.FindAll(x => x.type != "normal");
        achievementsOtherLst = achievementsGeneralLst.FindAll(x => x.type == "normal");
    }

    private void UpdateItemGeneralData()
    {
        if(achievementItemGeneralUILst.Count>0)
        {
            for (int i = 0; i < achievementsGeneralLst.Count; i++)
            {
                achievementItemGeneralUILst[i].SetData(achievementsGeneralLst[i],this);
            }
        }
        else
        {
            foreach (var item in achievementsGeneralLst)
            {
                GameObject newItem = Instantiate(achievementItemUIPrefap, itemsGeneralTranform);
                AchievementItemUI achievementItemUI = newItem.GetComponent<AchievementItemUI>();
                achievementItemUI.SetData(item, this);
                achievementItemGeneralUILst.Add(achievementItemUI);
            }
        }
    }

    private void UpdateItemRecordData()
    {
        if (achievementItemRecordUILst.Count > 0)
        {
            for (int i = 0; i < achievementsRecordLst.Count; i++)
            {
                achievementItemRecordUILst[i].SetData(achievementsRecordLst[i], this);
            }
        }
        else
        {
            foreach (var item in achievementsRecordLst)
            {
                GameObject newItem = Instantiate(achievementItemUIPrefap, itemsRecordTranform);
                AchievementItemUI achievementItemUI = newItem.GetComponent<AchievementItemUI>();
                achievementItemUI.SetData(item, this);
                achievementItemRecordUILst.Add(achievementItemUI);
            }
        }
    }

    private void UpdateItemOtherData()
    {
        if (achievementItemOtherUILst.Count > 0)
        {
            for (int i = 0; i < achievementsOtherLst.Count; i++)
            {
                achievementItemOtherUILst[i].SetData(achievementsOtherLst[i], this);
            }
        }
        else
        {
            foreach (var item in achievementsOtherLst)
            {
                GameObject newItem = Instantiate(achievementItemUIPrefap, itemsOtherTranform);
                AchievementItemUI achievementItemUI = newItem.GetComponent<AchievementItemUI>();
                achievementItemUI.SetData(item, this);
                achievementItemOtherUILst.Add(achievementItemUI);
            }
        }
    }

    public void GetReward(Achievement achievement)
    {
        if (achievement != null && !achievement.isGotReward&& achievementLocalDataLst.Exists(x=>x.id==achievement.id))
        {
            int coin = LocalData.instance.GetCoin();

            LocalData.instance.SetCoin(coin+achievement.rewardValue);

            achievementLocalDataLst.Find(x=>x.id==achievement.id).isGotReward = true;

            LocalData.instance.SetAchiementLocalData(achievementLocalDataLst);

            UpdateDataAchivementLst();

            UpdateItemGeneralData();
            UpdateItemRecordData();
            UpdateItemOtherData();
        }
    }
}

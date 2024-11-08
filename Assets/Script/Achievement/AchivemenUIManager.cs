using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AchivemenUIManager : MonoBehaviour
{
    [SerializeField] private AchivementData achievementData;
    [SerializeField] private GameObject achievementItemUIPrefap;
    [SerializeField] private Transform itemsGeneralTranform;

    private List<AchievementLocalData> achievementLocalDataLst=new List<AchievementLocalData>();
    private List<Achievement> achievementsGeneralLst = new List<Achievement>();

    private List<AchievementItemUI> achievementItemUILst=new List<AchievementItemUI>();

    private void OnEnable()
    {
        achievementLocalDataLst = LocalData.instance.GetAchievementLocalData();

        achievementsGeneralLst = achievementData.achievementsData;
        foreach (var item in achievementsGeneralLst)
        {
            AchievementLocalData data = achievementLocalDataLst.Find(x => x.id == item.id);

            if (data!=null)
            {
                item.isUnlocked = true;
                item.isGotReward = data.isGotReward;
            }
        };
        //sort list by isUnlock=true and isGotReward=true first
        achievementsGeneralLst.Sort((a, b) =>
        {
            int unlockComp = b.isUnlocked.CompareTo(a.isUnlocked);
            if(unlockComp != 0) { return unlockComp; }
            return b.isGotReward.CompareTo(a.isGotReward);
        });

        UpdateItemGeneralData();
    }

    public void UpdateItemGeneralData()
    {
        if(achievementItemUILst.Count>0)
        {
            for (int i = 0; i < achievementsGeneralLst.Count; i++)
            {
                achievementItemUILst[i].SetData(achievementsGeneralLst[i]);
            }
        }
        else
        {
            foreach (var item in achievementsGeneralLst)
            {
                GameObject newItem = Instantiate(achievementItemUIPrefap, itemsGeneralTranform);
                AchievementItemUI achievementItemUI = newItem.GetComponent<AchievementItemUI>();
                achievementItemUI.SetData(item);
                achievementItemUILst.Add(achievementItemUI);
            }
        }
    }
}

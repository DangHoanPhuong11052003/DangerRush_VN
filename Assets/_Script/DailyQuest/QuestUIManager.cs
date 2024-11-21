using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{

    [Header("===Quest List===")]
    [SerializeField] private QuestData questData;
    [SerializeField] private Transform DailyQuestTranform;
    [SerializeField] private GameObject QuestItemPrefab;

    [Header("===Stage Daily Quest And Reward===")]
    [SerializeField] private List<ChestDailyReward> ChestDailyRewardList=new List<ChestDailyReward>();
    [SerializeField] private Slider TotalStageBar;
    [SerializeField] private TextMeshProUGUI TotalStageTxt;

    private List<Quest> dailyQuestDataLst=new List<Quest>();
    private List<QuestLocalData> questLocalDatasLst=new List<QuestLocalData>();
    private List <QuestItem> questItemList=new List<QuestItem>();

    private int totalStageData;
    List<int> idChestCollectedRewardLst = new List<int>();

    private void OnEnable()
    {
        UpdateDailyQuestData();
        UpdateDailyQuestItem();

        UpdateTotalPointDailyQuestData();
    }

    //Total stage and reward daily quest
    private void UpdateTotalPointDailyQuestData()
    {
        totalStageData=LocalData.instance.GetTotalStageDailyQuest();
        idChestCollectedRewardLst = LocalData.instance.GetIdChestDailyQuestRewardCollected();

        TotalStageTxt.text=totalStageData.ToString();
        TotalStageBar.value = totalStageData;
        foreach (var item in ChestDailyRewardList)
        {
            if (idChestCollectedRewardLst.Contains(item.GetId()))
            {
                item.SetData(this, 2);
            }
            else
            {
                item.SetData(this, item.GetTotalPointCollect()<=totalStageData?1:0);
            }
        }
    }

    public void GetRewardDailyQuest(int idChest,int quantityFishbone)
    {
        if (!idChestCollectedRewardLst.Contains(idChest))
        {
            idChestCollectedRewardLst.Add(idChest);
            LocalData.instance.SetIdChestDailyQuestRewardCollected(idChestCollectedRewardLst);

            int coin = LocalData.instance.GetCoin();
            LocalData.instance.SetCoin(coin+quantityFishbone);

            UpdateTotalPointDailyQuestData();
        }
    }

    //Daily quest UI
    private void UpdateDailyQuestData()
    {
        dailyQuestDataLst.Clear();
        questLocalDatasLst=LocalData.instance.GetQuestLocalDatas();

        foreach (var item in questData.questDataLst)
        {
            QuestLocalData questLocalData= questLocalDatasLst!=null?questLocalDatasLst.Find(x=>x.id == item.id):null;

            if (questLocalData != null)
            {
                Quest quest = new Quest(item);
                quest.currentStage=questLocalData.stage;
                quest.isSuccess=questLocalData.isSuccess;
                quest.isGotReward=questLocalData.isGotReward;

                dailyQuestDataLst.Add(quest);
            }
            else
            {
                dailyQuestDataLst.Add(new Quest(item));
            }
        }

        dailyQuestDataLst.Sort((a, b) =>
        {
            if (!a.isGotReward && !b.isGotReward)
            {
                return b.isSuccess.CompareTo(a.isSuccess);
            }
            else
            {
                return a.isGotReward.CompareTo(b.isGotReward);
            }
        });
    }

    private void UpdateDailyQuestItem()
    {
        if(questItemList.Count > 0)
        {
            for(int i = 0; i < dailyQuestDataLst.Count; i++)
            {
                questItemList[i].SetData(dailyQuestDataLst[i],this);
            }
        }
        else
        {
            foreach (var item in dailyQuestDataLst)
            {
                GameObject newItem = Instantiate(QuestItemPrefab, DailyQuestTranform);
                QuestItem newQuestItem = newItem.GetComponent<QuestItem>();
                newQuestItem.SetData(item,this);

                questItemList.Add(newQuestItem);
            }
        }
    }

    public void CollectPointQuestItem(Quest quest)
    {
        QuestLocalData questLocalData= questLocalDatasLst.Find(x => x.id == quest.id);

        if (questLocalData!=null&&!questLocalData.isGotReward)
        {
            int toltalStage = LocalData.instance.GetTotalStageDailyQuest();
            LocalData.instance.SetTotalStageDailyQuest(toltalStage + quest.points);

            questLocalData.isGotReward = true;
            LocalData.instance.SetQuestLocalDatas(questLocalDatasLst);
        }

        UpdateDailyQuestData();
        UpdateDailyQuestItem();
        UpdateTotalPointDailyQuestData();   
    }
}

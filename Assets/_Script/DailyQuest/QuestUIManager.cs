using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    [SerializeField] private QuestData questData;
    [SerializeField] private Transform DailyQuestTranform;
    [SerializeField] private GameObject QuestItemPrefab;

    private List<Quest> dailyQuestDataLst=new List<Quest>();
    private List<QuestLocalData> questLocalDatasLst=new List<QuestLocalData>();
    private List <QuestItem> questItemList=new List<QuestItem>();

    private void Start()
    {
        UpdateDailyQuestData();
    }

    private void UpdateDailyQuestData()
    {
        dailyQuestDataLst = questData.questDataLst;
        questLocalDatasLst=LocalData.instance.GetQuestLocalDatas();

        foreach (var item in dailyQuestDataLst)
        {
            QuestLocalData questLocalData=questLocalDatasLst.Find(x=>x.id == item.id);

            if (questLocalData != null)
            {
                item.currentStage=questLocalData.stage;
                item.isSuccess=questLocalData.isSuccess;
                item.isGotReward=questLocalData.isGotReward;
            }
        }

        dailyQuestDataLst.Sort((a, b) =>
        {
            int isSucc= b.isSuccess.CompareTo(a.isSuccess);
            if (isSucc!=0)
            {
                return isSucc;
            }
            return a.isGotReward.CompareTo(b.isGotReward);
        });
    }

    private void UpdateDailyQuestItem()
    {
        if(questItemList.Count <= 0)
        {
            foreach (var item in dailyQuestDataLst)
            {
                
            }
        }
    }
}

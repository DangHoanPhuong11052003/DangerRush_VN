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

    private void OnEnable()
    {
        UpdateDailyQuestData();
        UpdateDailyQuestItem();
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
            if (a.isGotReward && b.isGotReward && !a.isGotReward && !b.isGotReward)
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class DailyQuestManager : MonoBehaviour
{
    private string apiUrl = "https://timeapi.io/api/time/current/zone?timeZone=Etc%2FUTC";

    public static DailyQuestManager instance;

    [SerializeField] private QuestData questData;

    private List<Quest> questDataLst= new List<Quest>();
    private List<QuestLocalData> questLocalDataLst= new List<QuestLocalData>();

    private void OnEnable()
    {
        SetUpDateDailyQuest();
    }

    private void Start()
    {
        if(instance == null)
        {
            instance =this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        StartCoroutine(GetTimeData());
    }

    private void SetUpDateDailyQuest()
    {
        questLocalDataLst = LocalData.instance.GetQuestLocalDatas();

        foreach (var item in questData.questDataLst)
        {
            QuestLocalData questLocal= questLocalDataLst.Find(x=>x.id==item.id);

            if (questLocal!=null)
            {
                item.stage=questLocal.stage;
                item.isSuccess = questLocal.isSuccess;
                item.isGotReward=questLocal.isGotReward;
            }
        }
    }

    //public void Check

    private IEnumerator GetTimeData()
    {
        DateTime timeUtcNow;
        DateTime localTime=LocalData.instance.GetLocalTime();

        //check internet
        if(Application.internetReachability==NetworkReachability.NotReachable)
        {
            //get time in device
            timeUtcNow = DateTime.UtcNow;
        }
        else
        {
            //get time api
            using (UnityWebRequest request = new UnityWebRequest(apiUrl))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    timeUtcNow = DateTime.Parse(request.GetResponseHeader("date"));
                }
                else
                {
                    timeUtcNow = DateTime.UtcNow;
                }
            }
        }

        if(timeUtcNow.Date > localTime.Date)
        {
            ResetDailyQuest();

            localTime = timeUtcNow;
            LocalData.instance.SetLocalTime(localTime);
        }
        
    }

    private void ResetDailyQuest()
    {
        LocalData.instance.SetQuestLocalDatas(null);
    }



}

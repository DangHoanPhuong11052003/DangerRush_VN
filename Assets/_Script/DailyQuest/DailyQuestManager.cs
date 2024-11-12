using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class DailyQuestManager : MonoBehaviour
{
    private string apiUrl = "https://timeapi.io/api/time/current/zone?timeZone=Etc%2FUTC";

    public static DailyQuestManager instance;

    [SerializeField] private QuestData questData;

    private List<QuestLocalData> questLocalDataLst= new List<QuestLocalData>();

    private void OnEnable()
    {
        questLocalDataLst=LocalData.instance.GetQuestLocalDatas();
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

    //dành để kiểm tra các thành tựu dạng lấy tiến độ cao nhất được truyền vào
    public void UpdateHighStageQuest(int stage, DailyQuestsType dailyQuestsType)
    {
        //Lấy ra các phần tử có cùng kiểu cần kiểm tra trong ds tòan bộ nhiem vu
        foreach (var item in questData.questDataLst.FindAll(x => x.type == dailyQuestsType.ToString()))
        {
            //kiểm tra xem từng nhiệm vụ xem đã có tiến trình chưa
            QuestLocalData questLocal = questLocalDataLst.Find(x => x.id.Equals(item.id));

            if (questLocal!=null)
            {
                //lưu lại tiến trình mới vào local nếu tiến độ lớn hơn
                if(stage>= questLocal.stage)
                {
                    questLocal.stage=stage;
                    questLocal.isSuccess = item.stage <= stage;
                    LocalData.instance.SetQuestLocalDatas(questLocalDataLst);
                }
            }
            else
            {
                //tạo mới tiến độ nếu chưa có
                questLocal = new QuestLocalData(item.id, stage, item.stage <= stage, false);

                questLocalDataLst.Add(questLocal);
                LocalData.instance.SetQuestLocalDatas(questLocalDataLst);
            }
        }
    }

    //dành để kiểm tra các thành tựu dạng cộng dồn số tiến độ được truyền vào
    public void AccumulateStageQuest(int stage, DailyQuestsType dailyQuestsType)
    {
        //Lấy ra các phần tử có cùng kiểu cần kiểm tra trong ds tòan bộ nhiem vu
        foreach (var item in questData.questDataLst.FindAll(x => x.type == dailyQuestsType.ToString()))
        {
            //kiểm tra xem từng nhiệm vụ xem đã có tiến trình chưa
            QuestLocalData questLocal = questLocalDataLst.Find(x => x.id.Equals(item.id));

            if (questLocal != null)
            {
                //cộng dồn tiến độ nếu đã có
                questLocal.stage += stage;
                questLocal.isSuccess = item.stage <= stage;
                
            }
            else
            {
                //tạo mới tiến độ nếu chưa có
                questLocal = new QuestLocalData(item.id, stage, item.stage <= stage, false);

                questLocalDataLst.Add(questLocal);
            }
                
            LocalData.instance.SetQuestLocalDatas(questLocalDataLst);
        }
    }

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

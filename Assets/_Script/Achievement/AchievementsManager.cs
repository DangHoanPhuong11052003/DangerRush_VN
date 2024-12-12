using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class AchievementsManager : MonoBehaviour
{
    [Serializable]
    private class StageOfAchievement
    {
        public Achiverments achiverments;
        public float stage;
    }

    [SerializeField] private AchievementsNotification notification;
    [SerializeField] private Animator notificationAnimator;

    [SerializeField] private List<StageOfAchievement> scoreAchievementsLstData;
    [SerializeField] private List<StageOfAchievement> getCoinAchievementsLstData;
    [SerializeField] private List<StageOfAchievement> takePowerAchievementLstData;

    [SerializeField] Canvas Canvas;
    

    public static AchievementsManager instance;
    private float timer;
    private float timeDelayNotifi;
    private List<string> notificationLst = new List<string>();
    private List<AchievementLocalData> achievementLocalDataLst = new List<AchievementLocalData>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        timeDelayNotifi = notificationAnimator.runtimeAnimatorController.animationClips[0].length+0.5f;

        achievementLocalDataLst=LocalData.instance.GetAchievementLocalData();

        EventManager.Subscrice(KeysEvent.GainCoin.ToString(), CheckCoinGainAchievement);
        EventManager.Subscrice(KeysEvent.PowerTaked.ToString(), CheckTakePowerAchievement);
    }
    private void OnDisable()
    {
        EventManager.UnSubscrice(KeysEvent.GainCoin.ToString(), CheckCoinGainAchievement);
        EventManager.UnSubscrice(KeysEvent.PowerTaked.ToString(), CheckTakePowerAchievement);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (notificationLst.Count > 0&& timer <= 0)
        {
            ShowAchievementsNotification();

            timer = timeDelayNotifi;
        }
    }

    private void ShowAchievementsNotification()
    {
        notification.gameObject.SetActive(true);
        notification.ShowAchievementsNotification(notificationLst[0]);

        notificationLst.RemoveAt(0);
    }

    public void UnlockAchievement(string titleAchievements)
    {
        bool isUnlock=achievementLocalDataLst.Exists(x=>x.id==titleAchievements);
        if(!isUnlock)
        {
            //thông báo nhận thành tựu 
            notificationLst.Add(titleAchievements);

            achievementLocalDataLst.Add(new AchievementLocalData(titleAchievements,false));
            LocalData.instance.SetAchiementLocalData(achievementLocalDataLst);
        }
    }

    public void CheckScoreAchievement(int score)
    {
        Record recordData=LocalData.instance.GetRecordData();

        if(score> recordData.score)
        {
            foreach (var item in scoreAchievementsLstData)
            {
                if (score < item.stage)
                {
                    return;
                }
                else if (!achievementLocalDataLst.Exists(x=>x.id==item.achiverments.ToString()))
                {
                    UnlockAchievement(item.achiverments.ToString());
                }
            }

            //save new score record 
            recordData.score = score;
            LocalData.instance.SetRecordData(recordData);
        }
    }
    public void CheckCoinGainAchievement(object parameter)
    {
        Record recordData = LocalData.instance.GetRecordData();

        foreach (var item in getCoinAchievementsLstData)
        {
            if (recordData.coin < item.stage)
            {
                return;
            }
            else if (!achievementLocalDataLst.Exists(x => x.id == item.achiverments.ToString()))
            {
                UnlockAchievement(item.achiverments.ToString());
            }
        }
    }
    public void CheckTakePowerAchievement(object parameter)
    {
        Record recordData = LocalData.instance.GetRecordData();
        recordData.takedPower++;
        LocalData.instance.SetRecordData(recordData);

        foreach (var item in takePowerAchievementLstData)
        {
            if (recordData.takedPower < item.stage)
            {
                return;
            }
            else if (!achievementLocalDataLst.Exists(x => x.id == item.achiverments.ToString()))
            {
                UnlockAchievement(item.achiverments.ToString());
            }
        }
    }
}

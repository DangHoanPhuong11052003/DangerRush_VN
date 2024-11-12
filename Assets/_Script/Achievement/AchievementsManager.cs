using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class AchievementsManager : MonoBehaviour
{
    [Serializable]
    private class ScoreAchievement
    {
        public Achiverments achiverments;
        public int score;
    }

    [SerializeField] private AchievementsNotification notification;
    [SerializeField] private Animator notificationAnimator;

    [SerializeField] private List<ScoreAchievement> scoreAchievementsLstData;
    

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
            Destroy(this);
        }

        timeDelayNotifi = notificationAnimator.runtimeAnimatorController.animationClips[0].length+0.5f;

        achievementLocalDataLst=LocalData.instance.GetAchievementLocalData();
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
            foreach (ScoreAchievement item in scoreAchievementsLstData)
            {
                if (score < item.score)
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
}

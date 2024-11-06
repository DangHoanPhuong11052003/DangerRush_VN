using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    [SerializeField] private AchivementData achivementData;
    [SerializeField] private AchievementsNotification notification;
    [SerializeField] private List<string> notificationLst = new List<string>();
    [SerializeField] private Animator notificationAnimator;

    public static AchievementsManager instance;
    private float timer;
    private float timeDelayNotifi;

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

    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            notificationLst.Add("agagaa " + i);
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (notificationLst.Count > 0&& timer <= 0)
        {
            ShowAchievementsNotification(notificationLst[0]);
            notificationLst.RemoveAt(0);

            timer = timeDelayNotifi;
        }
    }

    private void ShowAchievementsNotification(string title)
    {
        notification.gameObject.SetActive(true);
        notification.ShowAchievementsNotification(title);
    }


}

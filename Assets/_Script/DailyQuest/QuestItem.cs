using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PointValueUI;
    [SerializeField] private TextMeshProUGUI TitleQuestUI;
    [SerializeField] private TextMeshProUGUI StageValueUI;
    [SerializeField] private TextMeshProUGUI StatusQuestUI;
    [SerializeField] private Slider StageBarValue;
    [SerializeField] private Image StatusImgUI;
    [SerializeField] private Sprite IncompleteImg;
    [SerializeField] private Sprite CollectedImg;
    [SerializeField] private GameObject ButtonCollect;

    private Quest quest;
    private QuestUIManager QuestUIManager;

    public void SetData(Quest questValue, QuestUIManager questUIManager)
    {
        quest = questValue;
        this.QuestUIManager = questUIManager;

        TitleQuestUI.text = quest.name;
        PointValueUI.text = questValue.points.ToString();
        StageValueUI.text = $"{questValue.currentStage}/{questValue.stage}";
        StageBarValue.maxValue = questValue.stage;
        StageBarValue.value = questValue.currentStage;
        if (questValue.isSuccess&&!questValue.isGotReward)
        {
            ButtonCollect.SetActive(true);
            StatusImgUI.gameObject.SetActive(false);
        }
        else
        {
            if(questValue.isSuccess)
            {
                ButtonCollect.SetActive(false);
                StatusImgUI.gameObject.SetActive(true);
                StatusImgUI.sprite = CollectedImg;
                StatusQuestUI.text = "Collected";
            }
            else
            {
                ButtonCollect.SetActive(false);
                StatusImgUI.gameObject.SetActive(true);
                StatusImgUI.sprite = IncompleteImg;
                StatusQuestUI.text = "Incomplete";
            }
        }
    }

    public void CollectPoint()
    {
        QuestUIManager.CollectPointQuestItem(quest);
    }
}

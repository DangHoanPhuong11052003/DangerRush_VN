using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestDailyReward : MonoBehaviour
{
    [Header("===Chest data===")]
    [SerializeField] private int idChest;
    [SerializeField] private int totalPointCollect;
    [SerializeField] private int quantityFishBoneReward;
    [Header("======")]
    [SerializeField] private Image UnButtonStatusImg;
    [SerializeField] private GameObject ButtonReward;
    [SerializeField] private Sprite CloseChestImg;
    [SerializeField] private Sprite EmptyChestImg;

    private QuestUIManager QuestUIManager;

    public int GetId()
    {
        return idChest;
    }
    public int GetTotalPointCollect()
    {
        return totalPointCollect;
    }

    public void SetData(QuestUIManager questUIManager,int status)
    {
        QuestUIManager=questUIManager;

        switch (status)
        {
            case 0:
                {
                    UnButtonStatusImg.gameObject.SetActive(true);
                    ButtonReward.SetActive(false);

                    UnButtonStatusImg.sprite = CloseChestImg;
                    break;
                }
            case 1:
            {
                UnButtonStatusImg.gameObject.SetActive(false);
                ButtonReward.SetActive(true);
                break;
            }
            case 2:
            {
                UnButtonStatusImg.gameObject.SetActive(true);
                ButtonReward.SetActive(false);

                UnButtonStatusImg.sprite= EmptyChestImg;
                break;
            }
            default:return;
        }
    }

    public void GetReward()
    {

    }
}

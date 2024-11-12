using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILoseManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI fishBoneValue;
    [SerializeField] private TextMeshProUGUI fishBoneBonusValue;

    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private Achiverments achiverments_FirstDead;

    private void OnEnable()
    {
        score.text =Mathf.CeilToInt(playerManager.score) +"M";
        fishBoneValue.text= playerManager.coin.ToString();
        fishBoneBonusValue.text = "+"+fishBoneValue.text;

        //check achiement when player lose
        AchievementsManager.instance.UnlockAchievement(achiverments_FirstDead.ToString());
        AchievementsManager.instance.CheckScoreAchievement(Mathf.CeilToInt(playerManager.score));

    }
}

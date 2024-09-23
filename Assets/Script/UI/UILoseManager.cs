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

    private void OnEnable()
    {
        score.text =Mathf.FloorToInt(playerManager.score) +"M";
        fishBoneValue.text= playerManager.coin.ToString();
        fishBoneBonusValue.text = "+"+fishBoneValue.text;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILoseManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI fishBoneValue;
    [SerializeField] private TextMeshProUGUI fishBoneBonusValue;

    private void OnEnable()
    {
        score.text =Mathf.CeilToInt(GameManager.instance.score) +"M";
        fishBoneValue.text=GameManager.instance.coin.ToString();
        fishBoneBonusValue.text = fishBoneValue.text;
    }
}

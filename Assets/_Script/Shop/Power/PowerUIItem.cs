using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUIItem : MonoBehaviour
{
    [SerializeField] private StackSlider StackSlider;
    [SerializeField] private Image iconPower;
    [SerializeField] private GameObject fullUpgradeImg;
    [SerializeField] private TextMeshProUGUI namePowerTxt;
    [SerializeField] private TextMeshProUGUI desPowerTxt;
    [SerializeField] private TextMeshProUGUI priceValueTxt;
    [SerializeField] private GameObject ButtonBuy;

    private PowerStoreManager PowerStoreManager;
    private Power power;

    public void SetData(Power powerItem, PowerStoreManager powerStoreManager)
    {
        this.PowerStoreManager = powerStoreManager;
        this.power= powerItem;

        if (powerItem != null)
        {
            StackSlider.SetStacksActiveAndTxtInfor(powerItem.level, powerItem.lst_timeActiveByLevel[power.level-1].ToString()+"s");
            iconPower.sprite = powerItem.icon;
            namePowerTxt.text = powerItem.name;
            desPowerTxt.text = powerItem.description;
            priceValueTxt.text = powerItem.lst_priceByLevel[powerItem.level].ToString();
            if(powerItem.level==powerItem.maxLevel)
            {
                ButtonBuy.SetActive(false);
                fullUpgradeImg.SetActive(true);
            }
            else
            {
                ButtonBuy.SetActive(true);
                fullUpgradeImg.SetActive(false);
            }
        }
    }

    public void UpgradePower()
    {
        PowerStoreManager.UpgradePower(power);
    }
}

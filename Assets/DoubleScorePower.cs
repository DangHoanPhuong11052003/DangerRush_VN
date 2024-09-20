using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class DoubleScorePower : PowerItem
{
    private int maxLevel = 5;
    private int priceInsreasePerLevel = 150;
    public override string GetDesByCurrentLv(int level)
    {
        int timerBuff = 0;

        switch (level)
        {
            case 1: timerBuff = 5; break;
            case 2: timerBuff = 6; break;
            case 3: timerBuff = 7; break;
            case 4: timerBuff = 8; break;
            case 5: timerBuff = 10; break;
            default: return null;
        }

        return $"Level {level}: Effect activation time {timerBuff}s";
    }

    public override int GetPrice()
    {
        return price + (level * priceInsreasePerLevel);
    }

    public override bool UpgradePower()
    {
        if(level < maxLevel)
        {
            level++;
            return true;
        }
        return false;
    }
}

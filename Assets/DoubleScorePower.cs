using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScorePower : PowerItem
{
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
        }

        return $"Thời gian kích hoạt hiệu ứng: {timerBuff}s";
    }
}

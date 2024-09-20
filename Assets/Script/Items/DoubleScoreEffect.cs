using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreEffect : PowerEffect
{
    private float timerDeActiveBuff = 0;
    [SerializeField] PlayerManager playerManager;

    public override void SetData(PowerData data)
    {
        level=data.level;

        switch(level)
        {
            case 2: timeBuff += 1; break;
            case 3: timeBuff += 1; break;
            case 4: timeBuff += 1; break;
            case 5: timeBuff += 1; break;
            default: return;
        }
    }

    private void OnEnable()
    {
        timerDeActiveBuff = timeBuff;

        playerManager.isDoubleScore = true;
    }


    private void Update()
    {
        timerDeActiveBuff -= Time.deltaTime;
        if(timerDeActiveBuff <= 0)
        {
            playerManager.isDoubleScore = false;
            gameObject.SetActive(false);
        }
    }
}

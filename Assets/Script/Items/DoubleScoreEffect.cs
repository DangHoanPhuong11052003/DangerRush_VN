using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreEffect : MonoBehaviour
{
    private float timeDeActiveBuff = 8f;
    private float timerDeActiveBuff = 0;
    [SerializeField] PlayerManager playerManager;

    private void OnEnable()
    {
        timerDeActiveBuff = timeDeActiveBuff;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreEffect : MonoBehaviour
{
    private float timeDeActiveBuff = 8f;
    private float timerDeActiveBuff = 0;

    private void OnEnable()
    {
        timerDeActiveBuff = timeDeActiveBuff;

        GameManager.instance.isDoubleScore = true;
    }


    private void Update()
    {
        timerDeActiveBuff -= Time.deltaTime;
        if(timerDeActiveBuff <= 0)
        {
            GameManager.instance.isDoubleScore = false;
            gameObject.SetActive(false);
        }
    }
}

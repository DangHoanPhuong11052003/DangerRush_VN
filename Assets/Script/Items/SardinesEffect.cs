using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SardinesEffect : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    private float timeActiveBuff = 5f;
    private float timer = 0f;

    private void OnEnable()
    {
        playerManager.isDoubleCoin = true;
        timer = timeActiveBuff;
    }

    private void Update()
    {
        timer-=Time.deltaTime;

        if(timer <= 0f)
        {
            playerManager.isDoubleCoin = false;
            gameObject.SetActive(false);
        }
    }
}

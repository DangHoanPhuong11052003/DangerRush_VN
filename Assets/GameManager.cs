using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    public static GameManager instance;

    [HideInInspector] public float score=0;
    [HideInInspector] public bool isDoubleScore=false;
    [HideInInspector] public float meter = 0;
    [HideInInspector] public int coin = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (playerManager.canMove)
        {
            IncreaseScoreAndMeter();
        }
    }

    private void IncreaseScoreAndMeter()
    {
        score += (isDoubleScore ? 2f * playerManager.transform.position.z - meter :  playerManager.transform.position.z - meter);
        meter = playerManager.transform.position.z;
    }

    public void IncreaseCoin(int quanity)
    {
        coin += quanity;
    }

    public void LoseGame()
    {
        if (coin > 0)
        {
            GameData gameData = LocalData.instance.GetGameData();
            gameData.coin += coin;
            LocalData.instance.SetData(gameData);
        }
    }
}

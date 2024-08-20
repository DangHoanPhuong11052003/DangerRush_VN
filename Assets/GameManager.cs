using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    public static GameManager instance;

    public float scroce=0;
    public bool isDoubleScore=false;

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
            IncreaseScore();
        }
    }

    public void IncreaseScore()
    {
        scroce += (isDoubleScore ? 1f * playerManager.currentSpeed * 2 : 1f * playerManager.currentSpeed) * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI fishbone;
    [SerializeField] private TextMeshProUGUI meter;
    [SerializeField] private List<GameObject> lst_Heart;
    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private GameObject LoseUI;
    [SerializeField] private GameObject PauseUI;

    private int currentHeart=0;

    private void Start()
    {
        currentHeart = playerManager.quantityLife;
    }

    private void Update()
    {
        score.text = "Score: "+Mathf.FloorToInt(playerManager.score);
        fishbone.text= Mathf.FloorToInt(playerManager.coin).ToString();
        meter.text= "Meter: "+Mathf.FloorToInt(playerManager.meter);

        if (currentHeart > playerManager.quantityLife)
        {
            currentHeart=playerManager.quantityLife;
            for (int i = 0; i < 3 - currentHeart; i++)
            {
                lst_Heart[i].SetActive(false);
            }
        }
    }

    public void OpenLoseUI()
    {
        LoseUI.SetActive(true);
    }

    public void OpenOrClosePauseUI(bool isOpen)
    {
        PauseUI.SetActive(isOpen);
        if(isOpen)
        {
            GameManager.instance.PauseGame();
        }
        else
        {
            GameManager.instance.ResumeGame();
        }
    }

    public void ResetGame()
    {
        GameManager.instance.LoadGame();
    }

    public void LoadMain()
    {
        GameManager.instance.LoadMain();
    }
}

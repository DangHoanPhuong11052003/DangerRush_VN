using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGamePlayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI fishbone;
    [SerializeField] private TextMeshProUGUI meter;
    [SerializeField] private List<GameObject> lst_Heart;
    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private GameObject LoseUI;
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject SettingUI;

    private int currentHeart=0;

    private void Start()
    {
        currentHeart = playerManager.quantityLife;
    }

    private void Update()
    {
        score.text = "Score: "+Mathf.CeilToInt(playerManager.score);
        fishbone.text= playerManager.coin.ToString();
        meter.text= "Meter: "+Mathf.CeilToInt(playerManager.meter);

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

    public void OpenSettingUI()
    {
        SettingUI.SetActive(true);
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

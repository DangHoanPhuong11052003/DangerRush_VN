using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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
    [SerializeField] private GameObject ReviveUI;

    private int currentHeart=0;

    private void Start()
    {
        currentHeart = playerManager.QuantityLife;
        UpdayeHeathUI(null);

        EventManager.Subscrice(KeysEvent.HeathUpdate.ToString(), UpdayeHeathUI);
    }

    private void OnDisable()
    {
        EventManager.UnSubscrice(KeysEvent.HeathUpdate.ToString(), UpdayeHeathUI);
    }

    private void Update()
    {
        score.text = "Score: "+Mathf.CeilToInt(playerManager.score);
        fishbone.text= playerManager.coin.ToString();
        meter.text= "Meter: "+Mathf.CeilToInt(playerManager.meter);
    }

    private void UpdayeHeathUI(object parameter)
    {
        if (currentHeart != playerManager.QuantityLife)
        {
            currentHeart = playerManager.QuantityLife;
            int i = currentHeart;
            foreach (var item in lst_Heart)
            {
                --i;
                if (i >= 0)
                    item.SetActive(true);
                else
                    item.SetActive(false);
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

    public void OpenOrCloseSettingUI(bool isOpen)
    {
        SettingUI.SetActive(isOpen);
    }

    public void OpenOrCloseReviveUI(bool isOpen)
    {
        if (isOpen)
        {
            GameManager.instance.PauseGame();
        }
        else
        {
            GameManager.instance.ResumeGame();
        }
        ReviveUI.SetActive(isOpen);
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

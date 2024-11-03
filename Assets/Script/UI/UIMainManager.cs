using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainManager : MonoBehaviour
{
    [SerializeField] private GameObject StoreMenu;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingMenu;
    public void LoadGame()
    {
        GameManager.instance.LoadGame();
    }

    public void OpenOrCloseStore(bool isOpen)
    {
        CloseAllMenu();
        StoreMenu.SetActive(isOpen);
    }

    public void OpenOrCloseMainMenu(bool isOpen)
    {
        CloseAllMenu();
        MainMenu.SetActive(isOpen);
    }

    public void OpenOrCloseSettingMenu(bool isOpen)
    {
        SettingMenu.SetActive(isOpen);
    }

    private void CloseAllMenu()
    {
        StoreMenu.SetActive(false);
        MainMenu.SetActive(false);
    }
}

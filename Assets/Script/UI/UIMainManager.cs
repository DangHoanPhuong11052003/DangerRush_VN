using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainManager : MonoBehaviour
{
    [SerializeField] private GameObject Store;
    public void LoadGame()
    {
        GameManager.instance.LoadGame();
    }

    public void OpenOrCloseStore(bool isOpen)
    {
        Store.SetActive(isOpen);
    }
}

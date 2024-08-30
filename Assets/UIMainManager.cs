using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainManager : MonoBehaviour
{
    public void LoadGame()
    {
        GameManager.instance.LoadGame();
    }
}

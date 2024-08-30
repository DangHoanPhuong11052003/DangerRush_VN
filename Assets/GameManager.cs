using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    

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

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void LoadMain()
    {
        ResumeGame();
        SceneManager.LoadScene("Main");
    }

    public void LoadGame()
    {
        ResumeGame();
        SceneManager.LoadScene("GamePlay");
    }

    public void LoadMenu()
    {
        ResumeGame();
        Debug.Log("load menu");
        return;
    }
}

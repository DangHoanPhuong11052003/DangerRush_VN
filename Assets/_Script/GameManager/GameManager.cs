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
        AudioManager.instance.SetMusicAudio(AudioManager.instance.MenuMusic);
    }

    public void LoadGame()
    {
        ResumeGame();
        SceneManager.LoadScene("GamePlay");
        AudioManager.instance.SetMusicAudio(AudioManager.instance.lst_gameMusic[Random.Range(0, AudioManager.instance.lst_gameMusic.Count-1)]);
    }

    public void LoadMenu()
    {
        ResumeGame();
        Debug.Log("load menu");
        return;
    }
}

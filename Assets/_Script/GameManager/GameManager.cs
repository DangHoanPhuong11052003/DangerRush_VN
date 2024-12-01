using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WorldCurver worldCurver;
    [SerializeField] [Range(-0.1f, 0.1f)]
    private float curveStrengthGamePlay = 0.01f;

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
        StartCoroutine(LoadMainIE());
    }

    private IEnumerator LoadMainIE()
    {
        ResumeGame();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");
        asyncLoad.allowSceneActivation = true; // Đảm bảo chuy

        AudioManager.instance.SetMusicAudio(AudioManager.instance.MenuMusic);

        while (!asyncLoad.isDone)
        {
            yield return null; // Chờ đến khi scene tải xong
        }

        worldCurver.SetCurver(0);
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameIE());
    }

    private IEnumerator LoadGameIE()
    {
        ResumeGame();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GamePlay");
        asyncLoad.allowSceneActivation = true; // Đảm bảo chuyển scene

        AudioManager.instance.SetMusicAudio(AudioManager.instance.lst_gameMusic[Random.Range(0, AudioManager.instance.lst_gameMusic.Count-1)]);

        while (!asyncLoad.isDone)
        {
            yield return null; // Chờ đến khi scene tải xong
        }

        worldCurver.SetCurver(curveStrengthGamePlay);
    }

    public void LoadMenu()
    {
        ResumeGame();
        Debug.Log("load menu");
        return;
    }
}

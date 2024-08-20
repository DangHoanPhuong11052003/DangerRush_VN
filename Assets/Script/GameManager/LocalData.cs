using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameData
{
    public int coin;

    public GameData(int coin)
    {
        this.coin = coin;  
    }
}

public class LocalData : MonoBehaviour
{
    public static LocalData instance;
    GameData gameData=new GameData(0);

    private string localPath = Application.dataPath + "/Saves";
    private string fileName = "GameData.json";
    string fullPath;

    private void Awake()
    {        
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        fullPath=Path.Combine(localPath,fileName);
        if (!Directory.Exists(localPath))
        {
            Directory.CreateDirectory(localPath);
        }

        LoadData();
    }

    public void SaveData()
    {
        string json=JsonUtility.ToJson(gameData);
        File.WriteAllText(fullPath, json);
        LoadData();
    }

    public void LoadData()
    {
        if(File.Exists(fullPath))
        {
            string jsonData=File.ReadAllText(fullPath);
            gameData=JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            SaveData();
        }
    }

    public GameData GetGameData()
    {
        return gameData;
    }

    public void SetData(GameData data)
    {
        gameData = data;
        SaveData();
    }

}

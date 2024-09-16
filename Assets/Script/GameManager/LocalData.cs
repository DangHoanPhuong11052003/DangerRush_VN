using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coin;
    public int currentCharacter;
    public List<CharacterData> lst_characterData=new List<CharacterData>();
    public List<PowerData> lst_powerData=new List<PowerData>();

    public GameData()
    {
        this.coin = 0;  
        this.currentCharacter = 0;
    }
}
[System.Serializable]
public class CharacterData
{
    public int id;
    public bool isUnlock;

    public CharacterData()
    {
        id = -1;
        isUnlock = false;
    }
    public CharacterData(int id, bool isUnlock)
    {
        this.id = id;
        this.isUnlock = isUnlock;
    }
}

[System.Serializable]
public class PowerData
{
    public int id;
    public int level;

    public PowerData()
    {
        id = -1;
        level = 1;
    }
    public PowerData(int id, int level)
    {
        this.id = id;
        this.level = level;
    }
}

public class LocalData : MonoBehaviour
{
    public static LocalData instance;
    GameData gameData=new GameData();

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


    // Get set Game Data
    public GameData GetGameData()
    {
        return gameData;
    }

    public void SetGameData(GameData data)
    {
        gameData = data;
        SaveData();
    }


    //Get Set CharacterData
    public List<CharacterData> GetCharacterData()
    {
        return gameData.lst_characterData;
    } 

    public void SetCharacterData(List<CharacterData> datas )
    {
        gameData.lst_characterData=datas;
        SaveData();
    }

    //Get set coin data
    public int GetCoin()
    {
        return gameData.coin;
    }

    public void SetCoin(int coin)
    {
        gameData.coin=coin;
        SaveData();
    }

    //Get set curent selected character
    public int GetCurrentChar()
    {
        return gameData.currentCharacter;
    }

    public void SetCurrentChar(int id)
    {
        gameData.currentCharacter=id;
        SaveData();
    }

    //Get set PowerData
    public List<PowerData> GetPowersData()
    {
        return gameData.lst_powerData;
    }

    public void SetPowerData(List<PowerData> powerDatas)
    {
        gameData.lst_powerData=powerDatas;
        SaveData();
    }

}

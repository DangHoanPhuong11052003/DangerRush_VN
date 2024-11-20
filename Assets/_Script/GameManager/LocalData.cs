using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coin;
    public int currentCharacter;
    public int currentAccessories;
    public int TotalStageDailyQuest;
    public string localTime=new DateTime().ToString();
    public Volume volume=new Volume();
    public Record record=new Record();
    public List<int> lst_idChestDailyRewardCollected=new List<int>();
    public List<int> lst_idCharacterOwnedData = new List<int>();
    public List<int> lst_idAccessoriesOwnedData = new List<int>();
    public List<PowerData> lst_powerData=new List<PowerData>();
    public List<AchievementLocalData> achievementUnlockDataLst =new List<AchievementLocalData>();
    public List<QuestLocalData> dailyQuestDataLst=new List<QuestLocalData>();

    public GameData()
    {
        coin = 0;  
        currentCharacter = 0;
        currentAccessories = -1;
        lst_idCharacterOwnedData.Add(0);
    }
}


public class LocalData : MonoBehaviour
{
    public static LocalData instance;
    private GameData gameData=new GameData();

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
    public List<int> GetCharacterOwnedData()
    {
        return gameData.lst_idCharacterOwnedData;
    } 

    public void SetCharacterOwnedData(List<int> datas )
    {
        gameData.lst_idCharacterOwnedData = datas;
        SaveData();
    }

    //Get set coin data
    public int GetCoin()
    {
        return gameData.coin;
    }

    public void SetCoin(int coin)
    {
        //change coin record
        if (coin > gameData.coin)
        {
            gameData.record.coin += coin - gameData.coin;

            DailyQuestManager.instance.AccumulateStageQuest(coin - gameData.coin, DailyQuestsType.getFishbone);
        }
        else {
            DailyQuestManager.instance.AccumulateStageQuest(gameData.coin - coin,DailyQuestsType.useFishbone);
        }

        gameData.coin=coin;
        SaveData();

        EventManager.NotificationToActions(KeysEvent.CoinUpdate.ToString(), coin);
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

    /////
    public List<int> GetIdAccessoriesOwnedData()
    {
        return gameData.lst_idAccessoriesOwnedData;
    }

    public void SetIdAccessoriesOwnedData(List<int> lst_idAccessoriesOwnedData)
    {
        gameData.lst_idAccessoriesOwnedData = lst_idAccessoriesOwnedData;
        SaveData();
    }
    /////
    public int GetCurrentIdAccessories()
    {
        return gameData.currentAccessories;
    }

    public void SetCurrentIdAccessories(int id)
    {
        gameData.currentAccessories=id;
        SaveData();
    }
    /////
    public Volume GetVolume()
    {
        return gameData.volume;
    }

    public void SetVolume(Volume volume)
    {
        gameData.volume=volume; 
        SaveData();
    }
    /////
    public List<AchievementLocalData> GetAchievementLocalData()
    {
        return gameData.achievementUnlockDataLst;
    }

    public void SetAchiementLocalData(List<AchievementLocalData> data)
    {
        gameData.achievementUnlockDataLst=data;
        SaveData();
    }
    /////
    public Record GetRecordData()
    {
        return gameData.record;
    }

    public void SetRecordData(Record newRecord)
    {
        gameData.record=newRecord;
        SaveData();
    }

    /////////
    public DateTime GetLocalTime()
    {
        return DateTime.Parse(gameData.localTime);
    }

    public void SetLocalTime(DateTime time)
    {
        gameData.localTime=time.ToString();
        SaveData();
    }

    //Get, set daily quest data
    public List<QuestLocalData> GetQuestLocalDatas()
    {
        return gameData.dailyQuestDataLst;
    }

    public void SetQuestLocalDatas(List<QuestLocalData> questLocalDatas)
    {
        gameData.dailyQuestDataLst=questLocalDatas;
        SaveData();
    }

    //Get, set stage daily quest
    public int GetTotalStageDailyQuest()
    {
        return gameData.TotalStageDailyQuest;
    }

    public void SetTotalStageDailyQuest(int stage)
    {
        gameData.TotalStageDailyQuest = stage;
        SaveData();
    }

    //Get, set id daily quest chest collected reward
    public List<int> GetIdChestDailyQuestRewardCollected()
    {
        return gameData.lst_idChestDailyRewardCollected;
    }

    public  void SetIdChestDailyQuestRewardCollected(List<int> idChestLst )
    {
        gameData.lst_idChestDailyRewardCollected=idChestLst;    
        SaveData();
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

[Serializable]
public class Volume
{
    public float master;
    public float music;
    public float sfx;

    public Volume()
    {
        master = 1;
        music = 1;
        sfx = 1;
    }
}

[Serializable]
public class Record
{
    public int score;
    public int coin;
    public int quantityChar;

    public Record()
    {
        score = 0;
        coin = 0;
        quantityChar = 1;
    }
}

[Serializable]
public class AchievementLocalData
{
    public string id;
    public bool isGotReward;

    public AchievementLocalData()
    {
        id = "";
        isGotReward = false;
    }

    public AchievementLocalData(string id, bool isGotReward)
    {
        this.id = id;
        this.isGotReward = isGotReward;
    }
}

[Serializable]
public class QuestLocalData
{
    public int id;
    public int stage;
    public bool isSuccess;
    public bool isGotReward;

    public QuestLocalData()
    {
        id = -1;
        stage = 0;
        isSuccess = false;
        isGotReward= false;
    }

    public QuestLocalData(int id, int stage, bool isSuccess, bool isGotReward)
    {
        this.id = id;
        this.stage = stage;
        this.isSuccess = isSuccess;
        this.isGotReward = isGotReward;
    }
}

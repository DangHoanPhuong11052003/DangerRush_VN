using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest
{
    public int id;
    public string name;
    public string type;
    public int points;
    public int stage;
    public int currentStage;
    public bool isSuccess;
    public bool isGotReward;

    public Quest(Quest quest)
    {
        this.id = quest.id;
        this.name = quest.name;
        this.type = quest.type;
        this.points = quest.points;
        this.stage = quest.stage;
        this.currentStage = quest.currentStage;
        this.isSuccess = quest.isSuccess;
        this.isGotReward = quest.isGotReward;
    }
}

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
    public bool isSuccess;
    public bool isGotReward;
}

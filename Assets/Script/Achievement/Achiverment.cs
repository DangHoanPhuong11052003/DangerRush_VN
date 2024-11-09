using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[Serializable]
public class Achievement
{
    public string id;
    public string type;
    public string title;
    public string description;
    public int rewardValue;
    public bool isUnlocked;
    public bool isGotReward;
}


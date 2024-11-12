using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using System;
using System.IO;

[CustomEditor(typeof(QuestData))]
public class QuestDataEdit : Editor
{
    private QuestData data;
    private void OnEnable()
    {
        data = (QuestData)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate type enum"))
        {
            GenerateEnum();
        }
    }

    private void GenerateEnum()
    {
        string filePath = Path.Combine(Application.dataPath, "Saves/Achiverments.cs");
        string code = "public enum DailyQuestsType {";
        foreach (var item in data.questDataLst)
        {
            code += item.type + ",";
        }
        code += "}";

        File.WriteAllText(filePath, code);
        AssetDatabase.ImportAsset("Asset/Saves/Achiverments.cs");
    }
}

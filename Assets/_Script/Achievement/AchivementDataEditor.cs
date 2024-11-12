using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AchivementData))]
public class AchivementDataEditor : Editor
{
    private AchivementData data;

    private void OnEnable()
    {
        data = (AchivementData)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate Enum"))
        {
            GenerateEnum();
        }
    }

    private void GenerateEnum()
    {
        string filePath = Path.Combine(Application.dataPath, "Saves/Achiverments.cs");
        string code = "public enum Achiverments {";
        foreach (Achievement achievement in data.achievementsData)
        {
            code += ""+achievement.id + ",";
        }
        code += "}";

        File.WriteAllText(filePath, code);
        AssetDatabase.ImportAsset("Assets/Saves/Achiverments.cs");
    }
}

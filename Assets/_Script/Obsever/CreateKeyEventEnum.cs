using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CreateAssetMenu]
public class CreateKeyEventEnum : ScriptableObject
{
    [SerializeField] public List<string> keyEventLst = new List<string>();


    [ContextMenu("Generate Key Event Enum")]
    private void GeneralEnum()
    {
        string filePath = Path.Combine(Application.dataPath, "Saves/KeysEvent.cs");
        string code = "public enum KeysEvent {";

        foreach (var item in keyEventLst)
        {
            code += item + ",";
        }
        code += "}";
        File.WriteAllText(filePath, code);
        AssetDatabase.ImportAsset("Assets/Saves/KeysEvent.cs");
    }
}
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestData : ScriptableObject
{
    [SerializeField]
    public List<Quest> questDataLst = new List<Quest>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AchivementData : ScriptableObject
{
    [SerializeField]
    public List<Achievement> achievementsData=new List<Achievement>();
}

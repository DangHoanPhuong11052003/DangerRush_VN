using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PowerAssetData : ScriptableObject
{
    [SerializeField] public List<Power> lst_power=new List<Power>();
}

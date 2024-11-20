using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AccessoriesData :ScriptableObject
{
    [SerializeField] public List<Accessory> Accessories = new List<Accessory>();
}

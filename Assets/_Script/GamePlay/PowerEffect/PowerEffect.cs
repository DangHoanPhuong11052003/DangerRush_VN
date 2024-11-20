using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class PowerEffect : MonoBehaviour
{
    public int id;
    public int level;
    public float timeBuff;
    public bool isActive;


    public abstract void SetData(PowerData data);

    public virtual void ActivePower()
    {
        EventManager.NotificationToActions(KeysEvent.PowerTaked.ToString(),1);
    }
}

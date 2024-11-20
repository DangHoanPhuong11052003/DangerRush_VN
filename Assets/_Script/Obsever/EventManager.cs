using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager 
{
    private static Dictionary<string, Action<object>> _DicEvent = new Dictionary<string, Action<object>>();
    
    public static void Subscrice(string eventName, Action<object> action)
    {
        if (!_DicEvent.ContainsKey(eventName))
        {
            _DicEvent[eventName] = null;
        }
        _DicEvent[eventName] += action;
    }

    public static void UnSubscrice(string eventName, Action<object> action)
    {
        if(_DicEvent.ContainsKey(eventName))
        {
            _DicEvent[eventName] -= action;
            if (_DicEvent[eventName] == null)
            {
                _DicEvent.Remove(eventName);
            }
        }
    }

    public static void NotificationToActions(string eventName, object parameter)
    {
        if(_DicEvent.ContainsKey(eventName))
        {
            _DicEvent[eventName].Invoke(parameter);
        }
    }
}

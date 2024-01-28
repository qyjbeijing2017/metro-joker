using System;
using System.Collections.Generic;

public class EventManager
{
    private static readonly Dictionary<EventName, HashSet<object>> callbacksByType = new();

    public static void RegisterCallback<T>(EventName name, Action<T> callback)
    {
        if (!callbacksByType.ContainsKey(name))
        {
            callbacksByType[name] = new HashSet<object>();
        }

        callbacksByType[name].Add(callback);
    }

    public static void UnregisterCallback<T>(EventName name, Action<T> callback)
    {
        if (!callbacksByType.ContainsKey(name))
        {
            return;
        }

        callbacksByType[name].Remove(callback);
    }

    public static void TriggerEvent<T>(EventName name, T data)
    {
        if (!callbacksByType.ContainsKey(name))
        {
            return;
        }

        foreach (var callback in callbacksByType[name])
        {
            (callback as Action<T>)?.Invoke(data);
        }
    }
}

public enum EventName
{
    RoleEnterStation,
    RoleLeaveStation,
    RoleEnterTrain,
    RoleLeaveTrain,
    PlaySkill
}
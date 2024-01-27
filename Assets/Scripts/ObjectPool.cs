using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private static Dictionary<string, Stack<MonoBehaviour>> pools = new();

    public static void InitPool<T>(string key, T target, int count) where T : MonoBehaviour
    {
        if (!pools.ContainsKey(key))
        {
            pools.Add(key, new Stack<MonoBehaviour>());
        }

        for (var i = 0; i < count; i++)
        {
            pools[key].Push(Object.Instantiate(target));
        }
    }

    public static void Push<T>(string key, T target) where T : MonoBehaviour
    {
        if (!pools.ContainsKey(key))
        {
            pools.Add(key, new Stack<MonoBehaviour>());
        }

        pools[key].Push(target);
        target.gameObject.SetActive(false);
    }

    public static T Pop<T>(string key) where T : MonoBehaviour
    {
        if (!pools.ContainsKey(key))
        {
            pools.Add(key, new Stack<MonoBehaviour>());
        }

        if (pools[key].Count == 0)
        {
            return Object.Instantiate(Resources.Load<T>(key.ToLower()));
        }

        var pop = pools[key].Pop();
        pop.gameObject.SetActive(true);
        return pop as T;
    }
}
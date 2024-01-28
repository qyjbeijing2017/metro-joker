using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private static Dictionary<string, Stack<MonoBehaviour>> pools = new();

    private static Transform tPoolRoot;

    private static Transform GetPoolRoot()
    {
        if (tPoolRoot == null)
        {
            var mono = GameObject.FindObjectOfType<PoolRoot>(true);
            if (mono == null)
            {
                var go = new GameObject("PoolRoot");
                mono = go.AddComponent<PoolRoot>();
            }

            tPoolRoot = mono.transform;
        }

        return tPoolRoot;
    }

    public static void Push<T>(string key, T target) where T : MonoBehaviour
    {
        if (!pools.ContainsKey(key))
        {
            pools.Add(key, new Stack<MonoBehaviour>());
        }

        pools[key].Push(target);
        target.transform.SetParent(GetPoolRoot());
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
            return Object.Instantiate(Resources.Load<T>(key.ToLower()), GetPoolRoot());
        }

        var pop = pools[key].Pop();
        pop.gameObject.SetActive(true);
        return pop as T;
    }

    // Find All Object in Scene, and push them into the pool
    public static void Collect<T>(string key) where T : MonoBehaviour
    {
        if (!pools.ContainsKey(key))
        {
            pools.Add(key, new Stack<MonoBehaviour>());
        }

        var objects = GameObject.FindObjectsOfType<T>(true);
        foreach (var obj in objects)
        {
            Push(key, obj);
        }
    }
    
    public static void Reset()
    {
        pools.Clear();
    }
}
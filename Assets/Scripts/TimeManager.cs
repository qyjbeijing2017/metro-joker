using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static float timeScale { get; private set; } = 1f;
    private static List<Action<float>> scaledTicks = new();
    private static List<Action<float>> ticks = new();

    public static void AddScaledTick(Action<float> tick)
    {
        scaledTicks.Add(tick);
    }

    public static void AddTick(Action<float> tick)
    {
        ticks.Add(tick);
    }

    public static void RemoveScaledTick(Action<float> tick)
    {
        scaledTicks.Remove(tick);
    }

    public static void RemoveTick(Action<float> tick)
    {
        ticks.Remove(tick);
    }

    public static void SetTimeScale(float scale)
    {
        timeScale = scale;
    }

    private void Update()
    {
        var dt = Time.deltaTime;
        foreach (var tick in scaledTicks)
        {
            tick(dt * timeScale);
        }

        foreach (var tick in ticks)
        {
            tick(dt);
        }
    }
}
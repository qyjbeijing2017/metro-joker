using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public bool isStuck { private set; get; } = false;
    public HashSet<Line> lines = new();

    public void AddLine(Line line)
    {
        lines.Add(line);
    }

    public void SetStuck(bool stuck)
    {
    }
}
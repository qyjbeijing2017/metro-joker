using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Station : MonoBehaviour
{
    public bool isStuck { private set; get; } = false;
    public HashSet<Line> lines = new();
    public List<IRoleBase> roles = new();

    public void AddLine(Line line)
    {
        lines.Add(line);
    }

    public List<IRoleBase> GetRoles(Line line, bool reverse)
    {
        return roles.Where(i => i.line == line && i.reverse == reverse).ToList();
    }

    public void SetStuck(bool stuck)
    {
    }

    public bool IsTerminalOnLine(Line line)
    {
        if (line.isRing)
            return false;

        var index = line.stations.IndexOf(this);
        var isEnd = index == 0 || index == line.stations.Count - 1;
        return isEnd;
    }

    public bool IsOnlyTerminal()
    {
        return lines.All(IsTerminalOnLine);
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Station : MonoBehaviour
{
    public bool isStuck { private set; get; } = false;
    public List<Line> lines = new();
    public List<IRoleBase> roles = new();

    public void AddLine(Line line)
    {
        if (lines.Contains(line))
            return;

        lines.Add(line);
    }

    public void AddRole(IRoleBase role)
    {
        if (roles.Contains(role))
        {
            Debug.LogError("role already in station");
            return;
        }

        roles.Add(role);
    }

    public void RemoveRole(IRoleBase role)
    {
        if (!roles.Contains(role))
        {
            Debug.LogError("role not in station");
            return;
        }

        roles.Remove(role);
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
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
        return roles.Where(i => i.next.line == line && i.next.reverse == reverse).ToList();
    }

    public void SetStuck(bool stuck)
    {
    }
}
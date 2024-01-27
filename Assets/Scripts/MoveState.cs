using System;

[Serializable]
public class MoveState
{
    public Line line;
    public Station station;
    public bool reverse;
    public bool stay;

    public override string ToString()
    {
        return $"line:{line.name} station:{station.name} reverse:{reverse} stay:{stay}";
    }

    public bool IsAtTerminal()
    {
        var index = line.stations.IndexOf(station);
        var isEnd = reverse && index == 0 || !reverse && index == line.stations.Count - 1;
        return isEnd;
    }
}
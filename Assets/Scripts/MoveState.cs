public class MoveState
{
    public Line line;
    public Station station;
    public bool reverse;
    public bool stay;

    public override string ToString()
    {
        return $"{line.name} {station.name} {reverse} {stay}";
    }

    public bool IsAtTerminal()
    {
        var index = line.stations.IndexOf(this.station);
        var isEnd = reverse && index == 0 || !reverse && index == line.stations.Count - 1;
        return isEnd;
    }
}
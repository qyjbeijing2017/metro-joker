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
}
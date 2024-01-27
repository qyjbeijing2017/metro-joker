using UnityEngine;

public class Jocker : MonoBehaviour, IRoleBase
{
    public Train train { get; set; }
    public Station station { get; set; }
    public Line line { get; set; }
    public bool reverse { get; set; }
    public bool willStay { get; set; }

    public void Tick(float dt)
    {
    }
}
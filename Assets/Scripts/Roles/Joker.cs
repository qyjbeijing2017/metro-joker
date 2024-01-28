using UnityEngine;

public class Joker : MonoBehaviour, IRoleBase
{
    public Train train { get; set; }
    public Station station { get; set; }
    public Line line { get; set; }
    public bool reverse { get; set; }
    public bool willStay { get; set; }
    public bool wasArrested { get; set; } = false;

    [SerializeField] private Arrow _arrow;
    public Arrow arrow => _arrow;

    public void Tick(float dt)
    {
    }
}
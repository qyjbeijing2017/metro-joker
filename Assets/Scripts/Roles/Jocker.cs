using UnityEngine;

public class Jocker : MonoBehaviour, IRoleBase
{
    public MoveState current { get; set; }
    public MoveState next { get; set; }
    public Train train { get; set; }

    public void Tick(float dt)
    {
    }
}
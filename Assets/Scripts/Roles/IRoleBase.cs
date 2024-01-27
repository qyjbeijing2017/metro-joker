using UnityEngine;

public interface IRoleBase
{
    public MoveState current { get; set; }
    public MoveState next { get; set; }
    public Train train { get; set; }
    public Transform t => (this as MonoBehaviour).transform;

    public void Tick(float dt);

    public void GetOff(Station station)
    {
        if (train == null)
        {
            return;
        }

        current = new MoveState
        {
            station = station,
            line = null,
            reverse = false,
            stay = true,
        };

        next = null;
    }

    public void GetOn()
    {
        var mono = this as MonoBehaviour;
        mono.transform.SetParent(train.transform.parent);
        mono.transform.localPosition = Vector3.zero;
    }

    public void SetNext(Line line, bool reverse)
    {
        next = new MoveState()
        {
            line = line,
            reverse = reverse,
            stay = false,
            // station = 
        };
    }
}
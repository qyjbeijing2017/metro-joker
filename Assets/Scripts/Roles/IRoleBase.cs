using UnityEngine;

public interface IRoleBase
{
    public Train train { get; set; }
    public Station station { get; set; }
    public Line line { get; set; }
    public bool reverse { get; set; }
    public bool willStay { get; set; }
    public Transform t => (this as MonoBehaviour).transform;

    public void Tick(float dt);

    public void GetOff(Station station)
    {
        if (train == null)
        {
            return;
        }

        this.station = station;
        station.AddRole(this);
        line = null;
        reverse = false;
        train.RemoveRole(this);
        train = null;
        willStay = true;

        var trans = t;
        trans.SetParent(station.transform);
        trans.localPosition = Vector3.zero;
    }

    public void GetOn(Train train)
    {
        this.train = train;
        station.RemoveRole(this);
        train.AddRole(this);
        station = null;
        line = null;
        reverse = false;
        willStay = false;

        var trans = t;
        trans.SetParent(train.transform);
        trans.localPosition = Vector3.zero;
    }

    public void SetNext(Line line, bool reverse)
    {
        this.line = line;
        this.reverse = reverse;
        station = null;
        train = null;
        willStay = false;
    }
}
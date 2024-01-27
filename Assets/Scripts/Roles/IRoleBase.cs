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

    public void EnterStation(Station station)
    {
        this.station = station;
        station.AddRole(this);
        EventManager.TriggerEvent(EventName.RoleEnterStation, (station, this));
        line = null;
        reverse = false;
        if (train != null)
        {
            train.RemoveRole(this);
            EventManager.TriggerEvent(EventName.RoleLeaveTrain, (train, this));
        }

        train = null;
        willStay = true;

        var trans = t;
        trans.SetParent(station.transform);
        trans.localPosition = Vector3.zero;
    }

    public void EnterTrain(Train train)
    {
        this.train = train;
        if (station != null)
        {
            station.RemoveRole(this);
            EventManager.TriggerEvent(EventName.RoleLeaveStation, (station, this));
        }

        train.AddRole(this);
        EventManager.TriggerEvent(EventName.RoleEnterTrain, (train, this));
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
        // station = null;
        train = null;
        willStay = false;
    }
}
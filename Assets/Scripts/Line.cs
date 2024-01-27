using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public List<Station> stations = new();
    [SerializeField] private Color _color;
    public HashSet<Train> trains = new();
    public bool isRing;
    public float timeGap;
    public float timeSinceLastSpawn;
    public float trainSpeed;
    public float trainSpeedMultiplier;

    private void OnEnable()
    {
        TimeManager.AddScaledTick(Tick);
    }

    private void OnDisable()
    {
        TimeManager.RemoveScaledTick(Tick);
    }

    private void Tick(float dt)
    {
        foreach (var train in trains)
        {
            train.Tick(dt);
        }

        timeSinceLastSpawn += dt;
        if (timeSinceLastSpawn > timeGap)
        {
            timeSinceLastSpawn -= timeGap;
            SpawnTrain();
        }
    }

    private void SpawnTrain()
    {
        var t = ObjectPool.Pop<Train>("train");
        t.Spawn(this, new MoveState
        {
            line = this,
            station = stations[0],
            reverse = false,
        });

        var r = ObjectPool.Pop<Train>("train");
        r.Spawn(this, new MoveState
        {
            line = this,
            station = stations[^1],
            reverse = true,
        });
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        trainSpeedMultiplier = multiplier;
        foreach (var train in trains)
        {
            train.SetSpeedMultiplier(multiplier);
        }
    }

    public void AddTrain(Train train)
    {
        trains.Add(train);
    }

    public void RemoveTrain(Train train)
    {
        trains.Remove(train);
    }

    public void Init()
    {
        foreach (var s in stations)
        {
            s.AddLine(this);
        }
    }

    public bool GetNextMoveState(MoveState curState, out MoveState newState)
    {
        if (isRing)
        {
            return GetNextStateRing(curState, out newState);
        }
        else
        {
            return GetNextMoveStateNonRing(curState, out newState);
        }
    }

    public bool GetNextMoveStateNonRing(MoveState curState, out MoveState newState)
    {
        if (curState == null)
        {
            Debug.LogError("MoveState is null");
            newState = null;
            return false;
        }

        Debug.Log($"GetNextMoveState for {curState.station.name} {curState.reverse}");

        if (curState.line != this)
        {
            Debug.LogError("MoveState is not on this line");
            newState = null;
            return false;
        }

        if (curState.stay)
        {
            Debug.LogError("MoveState is not moving");
            newState = curState;
            return true;
        }

        var curStationIndex = stations.IndexOf(curState.station);
        if (curStationIndex == -1)
        {
            Debug.LogError("MoveState is not on this line");
            newState = null;
            return false;
        }

        var isFirst = curStationIndex == 0;
        var isLast = curStationIndex == stations.Count - 1;

        // if not reverse, it should find next stop, or get backwards and set reverse to true
        if (!curState.reverse)
        {
            newState = new MoveState
            {
                line = this,
                station = stations[isLast ? curStationIndex - 1 : curStationIndex + 1],
                reverse = isLast,
                stay = false
            };

            Debug.Log($"next stop {newState.station.name} {newState.reverse}");

            return true;
        }

        newState = new MoveState
        {
            line = this,
            reverse = !isFirst,
            stay = false,
            station = stations[isFirst ? curStationIndex + 1 : curStationIndex - 1]
        };

        Debug.Log($"next stop {newState.station.name} {newState.reverse}");
        return true;
    }

    public bool GetNextStateRing(MoveState curState, out MoveState newState)
    {
        if (curState == null)
        {
            Debug.LogError("MoveState is null");
            newState = null;
            return false;
        }

        Debug.Log($"GetNextMoveState for {curState.station.name} {curState.reverse}");

        if (curState.line != this)
        {
            Debug.LogError("MoveState is not on this line");
            newState = null;
            return false;
        }

        if (curState.stay)
        {
            Debug.LogError("MoveState is not moving");
            newState = curState;
            return true;
        }

        var curStationIndex = stations.IndexOf(curState.station);
        if (curStationIndex == -1)
        {
            Debug.LogError("MoveState is not on this line");
            newState = null;
            return false;
        }

        var stationsCount = stations.Count;
        var nextIndex = (curStationIndex + (curState.reverse ? -1 : 1) + stationsCount) % stationsCount;
        newState = new MoveState
        {
            line = this,
            station = stations[nextIndex],
            reverse = curState.reverse,
            stay = false
        };

        Debug.Log($"next stop {newState.station.name} {newState.reverse}");
        return true;
    }
}
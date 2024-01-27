using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public List<Station> stations = new();
    [SerializeField] private Color _color;
    public HashSet<Train> trains = new();
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

    public bool GetNextMoveState(MoveState state, out MoveState newState)
    {
        if (state == null)
        {
            Debug.LogError("MoveState is null");
            newState = null;
            return false;
        }

        if (state.line != this)
        {
            Debug.LogError("MoveState is not on this line");
            newState = null;
            return false;
        }

        if (state.stay)
        {
            Debug.LogError("MoveState is not moving");
            newState = state;
            return true;
        }

        var index = stations.IndexOf(state.station);
        if (index == -1)
        {
            Debug.LogError("MoveState is not on this line");
            newState = null;
            return false;
        }

        // if not reverse, it should find next stop, or get backwards and set reverse to true
        if (!state.reverse)
        {
            var isLastStop = index == stations.Count - 1;
            newState = new MoveState
            {
                line = this,
                station = stations[isLastStop ? index - 1 : index + 1],
                reverse = isLastStop,
                stay = false
            };


            return true;
        }

        var isFirstStop = index == 0;
        newState = new MoveState
        {
            line = this,
            reverse = !isFirstStop,
            stay = false,
            station = stations[isFirstStop ? 1 : index - 1]
        };
        return true;
    }
}
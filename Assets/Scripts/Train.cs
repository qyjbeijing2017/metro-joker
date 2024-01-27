using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Train : MonoBehaviour
{
    public Line line;
    public Transform child;
    public List<IRoleBase> roles = new();

    public MoveState _c;

    private MoveState current
    {
        get => _c;
        set
        {
            _c = value;
            Debug.Log("set current " + value);
        }
    }

    private Vector3 posCur;

    public MoveState _n;

    private MoveState next
    {
        get => _n;
        set
        {
            _n = value;
            Debug.Log("set next " + value);
        }
    }

    public Vector3 posNext;
    public float distance;
    public Vector3 direction;
    public float speed = 1;
    public float speedMultiplier;
    public UnityEvent<bool> onReachStation = new();

    public void Spawn(Line line, MoveState start)
    {
        this.line = line;
        current = start;
        next = line.GetNextMoveState(start, out var state) ? state : null;
        CacheDistanceAndDirection();
        speed = line.trainSpeed;
        speedMultiplier = line.trainSpeedMultiplier;
        Debug.Log($"Spawn on station {start.station.name}");
        transform.position = current.station.transform.position;
        onReachStation.RemoveAllListeners();
    }


    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    private void OnEnable()
    {
        TimeManager.AddScaledTick(Tick);
    }

    private void OnDisable()
    {
        TimeManager.RemoveScaledTick(Tick);
    }

    public void Tick(float dt)
    {
        var selfTransform = transform;
        var remainingDistance = Vector3.Distance(selfTransform.position, posNext);
        var moveDistance = speed * (speedMultiplier + 1) * dt;
        if (remainingDistance <= moveDistance)
        {
            OnReachStation();
            var dif = moveDistance - remainingDistance;
            selfTransform.transform.position = posCur + direction * dif;
        }
        else
        {
            selfTransform.position += direction * moveDistance;
        }
    }

    public void MockDistance(float distance, bool reverse)
    {
        if (!reverse)
        {
            float totalDistance = 0;
            for (var i = 0; i < line.stations.Count - 1; i++)
            {
                var s1 = line.stations[i];
                var s2 = line.stations[i + 1];
                var distanceOfThisPart = Vector3.Distance(s1.transform.position, s2.transform.position);
                if (totalDistance + distanceOfThisPart > distance)
                {
                    var dif = distance - totalDistance;
                    var p1 = s1.transform.position;
                    var d = (s2.transform.position - p1).normalized;
                    transform.position = p1 + d * dif;
                    current = new MoveState
                    {
                        line = line,
                        station = s1,
                        reverse = false,
                        stay = false,
                    };
                    next = new MoveState
                    {
                        line = line,
                        station = s2,
                        reverse = false,
                        stay = false,
                    };
                    Debug.Log($"MockDistance Cur {current.station.name} Next {next.station.name}");
                    CacheDistanceAndDirection();
                    return;
                }

                totalDistance += distanceOfThisPart;
            }
        }
        else
        {
            float totalDistance = 0;
            for (var i = line.stations.Count - 1; i > 0; i--)
            {
                var s1 = line.stations[i];
                var s2 = line.stations[i - 1];
                var distanceOfThisPart = Vector3.Distance(s1.transform.position, s2.transform.position);
                if (totalDistance + distanceOfThisPart > distance)
                {
                    var dif = distance - totalDistance;
                    var p1 = s1.transform.position;
                    var d = (s2.transform.position - p1).normalized;
                    transform.position = p1 + d * dif;
                    current = new MoveState
                    {
                        line = line,
                        station = s1,
                        reverse = true,
                        stay = false,
                    };
                    next = new MoveState
                    {
                        line = line,
                        station = s2,
                        reverse = true,
                        stay = false,
                    };
                    return;
                }

                totalDistance += distanceOfThisPart;
            }
        }
    }

    private void CacheDistanceAndDirection()
    {
        posCur = current.station.transform.position;
        posNext = next.station.transform.position;
        distance = Vector3.Distance(posNext, posCur);
        direction = (posNext - posCur).normalized;
        transform.forward = direction;
    }

    private void OnReachStation()
    {
        current = next;
        var b = current.line.GetNextMoveState(current, out var state);
        if (!b)
        {
            throw new Exception("Should not happen");
        }

        // Debug.Log($"Cur {current.station.name} Next {state.station.name}");
        next = state;
        if (ShouldRecycle())
        {
            onReachStation.Invoke(true);
            return;
        }

        CacheDistanceAndDirection();
        KickPassengersStaying();
        CollectPassengers();
        onReachStation.Invoke(false);
    }

    private bool ShouldRecycle()
    {
        var isEnd = current.IsAtTerminal();
        if (!isEnd || line.isRing)
            return false;
        ObjectPool.Push("train", this);
        KickAllPassengers();
        return true;
    }

    private void CollectPassengers()
    {
        var roles = current.station.GetRoles(next.line, next.reverse);
        foreach (var role in roles)
        {
            role.EnterTrain(this);
        }
    }

    public void AddRole(IRoleBase role)
    {
        if (roles.Contains(role))
        {
            Debug.LogError("Already contains role");
            return;
        }

        roles.Add(role);
    }

    public void RemoveRole(IRoleBase role)
    {
        if (!roles.Contains(role))
        {
            Debug.LogError("Not contains role");
            return;
        }

        roles.Remove(role);
    }

    private void KickPassengersStaying()
    {
        for (var i = roles.Count - 1; i >= 0; i--)
        {
            var p = roles[i];
            if (!p.willStay)
                continue;
            p.EnterStation(current.station);
        }
    }

    private void KickAllPassengers()
    {
        foreach (var p in roles)
        {
            p.EnterStation(current.station);
        }
    }
}
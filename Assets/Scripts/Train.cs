using System;
using UnityEngine;
using UnityEngine.Events;

public class Train : MonoBehaviour
{
    public Line line;

    private MoveState _c;

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

    private MoveState _n;

    private MoveState next
    {
        get => _n;
        set
        {
            _n = value;
            Debug.Log("set next " + value);
        }
    }

    private Vector3 posNext;
    private float distance;
    private Vector3 direction;
    private float speed = 1;
    private float speedMultiplier = 1;
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
        if (current.stay)
        {
            Debug.Log("Stay");
            return;
        }

        var selfTransform = transform;
        var remainingDistance = Vector3.Distance(selfTransform.position, posNext);
        var moveDistance = speed * speedMultiplier * dt;
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
                    var direction = (s2.transform.position - s1.transform.position).normalized;
                    transform.position = s1.transform.position + direction * dif;
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
                    var direction = (s2.transform.position - s1.transform.position).normalized;
                    transform.position = s1.transform.position + direction * dif;
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
    }

    private void OnReachStation()
    {
        Debug.Log($"Reach station {current.station.name}");
        current = next;
        if (current.stay)
        {
            Debug.Log("Stay");
            next = null;
        }
        else
        {
            var b = current.line.GetNextMoveState(current, out var state);
            if (!b)
            {
                throw new Exception("Should not happen");
            }

            Debug.Log($"Cur {current.station.name} Next {state.station.name}");
            next = state;
            CacheDistanceAndDirection();
        }

        var isEnd = current.IsAtTerminal();
        onReachStation.Invoke(isEnd);
        ObjectPool.Push("train", this);
    }
}
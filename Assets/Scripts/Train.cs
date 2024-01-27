using System;
using UnityEngine;
using UnityEngine.Events;

public class Train : MonoBehaviour
{
    public Line line;
    private MoveState current;
    private Vector3 posCurrent;
    private MoveState next;
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
        next = start;
        speed = line.trainSpeed;
        speedMultiplier = line.trainSpeedMultiplier;
        onReachStation.RemoveAllListeners();
    }


    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    public void Tick(float dt)
    {
        if (current.stay)
            return;

        var selfTransform = transform;
        var remainingDistance = distance;
        var moveDistance = speed * speedMultiplier * dt;
        if (remainingDistance <= moveDistance)
        {
            OnReachStation();
            var dif = moveDistance - remainingDistance;
            selfTransform.transform.position = posCurrent + direction * dif;
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

    private void OnReachStation()
    {
        current = next;
        if (current.stay)
        {
            next = null;
        }
        else
        {
            var b = current.line.GetNextMoveState(current, out var state);
            if (!b)
            {
                throw new Exception("Should not happen");
            }

            next = state;
            posCurrent = current.station.transform.position;
            posNext = next.station.transform.position;
            distance = Vector3.Distance(posCurrent, posNext);
            direction = (posNext - posCurrent).normalized;
        }

        onReachStation.Invoke(current.station.isTerminal);
    }
}
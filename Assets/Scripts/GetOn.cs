using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(IRoleBase))]
public class GetOn : MonoBehaviour
{
    IRoleBase player;
    InputManager inputManager;
    [SerializeField]
    [Range(0, 1)]
    float deathArea = 0.5f;
    [SerializeField]
    [Range(0, 360)]
    float marchAngle = 45f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<IRoleBase>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.train != null && inputManager.getOff)
        {
            player.willStay = true;
            return;
        }
        if (player.train == null && inputManager.direction.magnitude > deathArea)
        {

            foreach (var line in player.station.lines)
            {
                var dir = getDirection(line, false);
                if (dir != Vector2.zero)
                {
                    var angle = Vector2.SignedAngle(inputManager.direction, dir);
                    if (angle < marchAngle && angle > -marchAngle)
                    {
                        player.SetNext(line, false);
                        return;
                    }
                }
                dir = getDirection(line, true);
                if (dir != Vector2.zero)
                {
                    var angle = Vector2.SignedAngle(inputManager.direction, getDirection(line, true));
                    if (angle < marchAngle && angle > -marchAngle)
                    {
                        player.SetNext(line, true);
                        return;
                    }
                }
            }
        }
        if (player.train == null && inputManager.getOff)
        {
            player.line = null;
        }
    }

    Vector3 getDirection(Line line, bool reverse)
    {
        var index = line.stations.FindLastIndex((s) => s == player.station);
        var nextIndex = index + (reverse ? -1 : 1);
        if (nextIndex < 0 || nextIndex >= line.stations.Count)
        {
            if (line.isRing)
            {
                nextIndex = (nextIndex + line.stations.Count) % line.stations.Count;
            }
            else
                return Vector2.zero;
        }
        var nextStation = line.stations[nextIndex];
        return nextStation.transform.position - player.station.transform.position;
    }
}
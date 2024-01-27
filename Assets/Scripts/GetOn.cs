using System.Collections;
using System.Collections.Generic;
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
        if(inputManager.direction.magnitude > deathArea) {
            Debug.Log(inputManager.direction);
        }
        if (player.train != null && inputManager.getOff)
        {
            player.willStay = true;
            return;
        }
        if (player.train == null && inputManager.direction.magnitude > deathArea)
        {

            foreach(var line in player.station.lines) {
                var angle = Vector2.SignedAngle(inputManager.direction, getDirection(line, false));
                if (angle < marchAngle && angle > -marchAngle)
                {
                    player.line = line;
                    player.reverse = false;
                    return;
                }
                angle = Vector2.SignedAngle(inputManager.direction, getDirection(line, true));
                if (angle < marchAngle && angle > -marchAngle)
                {
                    player.line = line;
                    player.reverse = true;
                    return;
                }
            }
        }
        if (player.train == null && inputManager.getOff)
        {
            player.line = null;
        }
    }

    Vector2 getDirection(Line line, bool reverse)
    {
        var index = line.stations.FindLastIndex((s) => s == player.station);
        var nextIndex = index + (reverse ? -1 : 1);
        if (nextIndex < 0 || nextIndex >= line.stations.Count)
        {
            return Vector2.zero;
        }
        var nextStation = line.stations[nextIndex];
        return nextStation.transform.position - player.station.transform.position;
    }
}

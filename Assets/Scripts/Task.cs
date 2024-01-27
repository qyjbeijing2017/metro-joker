using System;
using System.Collections;
using System.Collections.Generic;
using Roles;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
    Station station;
    [SerializeField]
    private string taskName = "Task";
    [SerializeField]
    private Color TaskColor = Color.white;
    public Color taskColor { get { return TaskColor; } }

    [SerializeField]
    private float ArriveTime2Finished = 5.0f;
    public float arriveTime2Finished { get { return ArriveTime2Finished; } }
    private float ArriveTime = 0.0f;
    public float arriveTime { get { return ArriveTime; } }

    public bool isRunning { get; private set; } = false;

    public UnityAction OnTaskFinished;


    // Start is called before the first frame update
    void Start()
    {
        station = GetComponent<Station>();
        TimeManager.AddScaledTick(OnTick);

    }
    private float deltaTime = 0;
    void OnTick(float deltaTime) {
        this.deltaTime = deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(IRoleBase role in station.roles) {
            if(role is Jocker) {
                ArriveTime += deltaTime;
                return;
            }
        }
        ArriveTime = 0;
    }

    void OnDestroy() {
        TimeManager.RemoveScaledTick(OnTick);
    }
}

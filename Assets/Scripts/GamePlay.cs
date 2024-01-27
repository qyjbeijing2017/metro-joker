using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    private List<Jocker> jokers;
    private List<Policeman> police;
    private List<Task> tasks;
    private List<ExitGate> exitGates;

    private int taskFinishedCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        jokers = new List<Jocker>(FindObjectsOfType<Jocker>());
        police = new List<Policeman>(FindObjectsOfType<Policeman>());
        tasks = new List<Task>(FindObjectsOfType<Task>());
        exitGates = new List<ExitGate>(FindObjectsOfType<ExitGate>());
        exitGates.ForEach(gate => gate.enabled = false);
        tasks.ForEach(task => task.OnTaskFinished += OnTaskFinished);
        exitGates.ForEach(gate => gate.onExit += OnExit);
    }

    void OnTaskFinished()
    {
        taskFinishedCount++;
        if (taskFinishedCount == tasks.Count)
        {
            exitGates.ForEach(gate => gate.enabled = true);
        }
    }


    void OnExit() {
        Debug.Log("Exit");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
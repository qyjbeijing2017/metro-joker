using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.iOS;

public class GamePlay : MonoBehaviour
{
    private List<Jocker> jokers;
    private List<Policeman> police;
    private List<Task> tasks;
    private List<ExitGate> exitGates;

    private List<Jocker> caughtJockers = new List<Jocker>();

    private int taskFinishedCount = 0;

    public UnityAction<bool> OnGameFinished;


    // Start is called before the first frame update
    void Start()
    {
        jokers = new List<Jocker>(FindObjectsOfType<Jocker>());
        police = new List<Policeman>(FindObjectsOfType<Policeman>());
        tasks = new List<Task>(FindObjectsOfType<Task>());
        exitGates = new List<ExitGate>(FindObjectsOfType<ExitGate>());
        exitGates.ForEach(gate => gate.gameObject.SetActive(false));
        tasks.ForEach(task => task.OnTaskFinished += OnTaskFinished);
        exitGates.ForEach(gate => gate.onExit += OnExit);
        police.ForEach(p => p.GetComponent<CatchJocker>().OnJockerCaught += OnJockerCaught);
    }

    void OnTaskFinished()
    {
        taskFinishedCount++;
        if (taskFinishedCount == tasks.Count)
        {
            Debug.Log("Game Finished");
            exitGates.ForEach(gate => gate.gameObject.SetActive(true));
            tasks.ForEach(task => task.gameObject.SetActive(false));
        }
    }

    void OnJockerCaught(Jocker jocker)
    {
        caughtJockers.Add(jocker);
        if (caughtJockers.Count == jokers.Count)
        {
            OnGameFinished(false);
        }
    }


    void OnExit() {
        OnGameFinished(true);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
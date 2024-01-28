using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.iOS;

public class GamePlay : MonoBehaviour
{
    private List<Joker> jokers;
    private List<Policeman> police;
    private List<Task> tasks;
    private List<ExitGate> exitGates;

    private List<Joker> caughtJockers = new List<Joker>();

    private int taskFinishedCount = 0;

    public UnityAction<bool> OnGameFinished;


    // Start is called before the first frame update
    void Start()
    {
        jokers = new List<Joker>(FindObjectsOfType<Joker>());
        police = new List<Policeman>(FindObjectsOfType<Policeman>());
        tasks = new List<Task>(FindObjectsOfType<Task>().Where(task => task.enabled));
        exitGates = new List<ExitGate>(FindObjectsOfType<ExitGate>());
        exitGates.ForEach(gate => gate.gameObject.SetActive(false));
        tasks.ForEach(task => task.OnTaskFinished += OnTaskFinished);
        exitGates.ForEach(gate => gate.onExit += OnExit);
        police.ForEach(p => p.GetComponent<CatchJocker>().OnJockerCaught += OnJockerCaught);
        Debug.Log("Task: " + tasks.Count);
    }

    void OnTaskFinished()
    {
        taskFinishedCount++;
        if (taskFinishedCount == tasks.Count)
        {
            // exitGates.ForEach(gate => gate.gameObject.SetActive(true));
            // tasks.ForEach(task => task.gameObject.SetActive(false));
            OnExit();
        }
    }

    void OnJockerCaught(Joker joker)
    {
        caughtJockers.Add(joker);
        if (caughtJockers.Count == jokers.Count)
        {
            Debug.Log("Game Finished");
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
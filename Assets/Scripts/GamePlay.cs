using System.Collections;
using System.Collections.Generic;
using Roles;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    private List<Jocker> jokers;
    private List<Policeman> police;
    private List<Task> tasks;


    // Start is called before the first frame update
    void Start()
    {
        jokers = new List<Jocker>(FindObjectsOfType<Jocker>());
        police = new List<Policeman>(FindObjectsOfType<Policeman>());
        tasks = new List<Task>(FindObjectsOfType<Task>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

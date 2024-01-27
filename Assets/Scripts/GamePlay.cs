using System.Collections;
using System.Collections.Generic;
using Roles;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    private List<Jocker> jokers;
    private List<Policeman> police;

    private 

    // Start is called before the first frame update
    void Start()
    {
        jokers = new List<Jocker>(FindObjectsOfType<Jocker>());
        police = new List<Policeman>(FindObjectsOfType<Policeman>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

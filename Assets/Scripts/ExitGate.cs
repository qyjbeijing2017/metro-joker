using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGate : MonoBehaviour
{
    private Station station;
    // Start is called before the first frame update
    void Start()
    {
        station = GetComponentInParent<Station>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

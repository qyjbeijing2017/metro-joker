using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Station))]
public class PlayerReborn : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    private Station station;
    // Start is called before the first frame update
    void Start()
    {
        station = GetComponent<Station>();
        player.GetComponent<IRoleBase>().EnterStation(station);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

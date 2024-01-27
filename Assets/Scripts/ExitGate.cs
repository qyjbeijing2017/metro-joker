using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Station))]
public class ExitGate : MonoBehaviour
{
    private Station station;
    public UnityAction onExit;
    // Start is called before the first frame update
    void Start()
    {
        station = GetComponentInParent<Station>();

        EventManager.RegisterCallback<(Station, IRoleBase)>(EventName.RoleEnterStation, (e) =>
        {
            if (e.Item1 == station && e.Item2 is Jocker)
            {
                onExit?.Invoke();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

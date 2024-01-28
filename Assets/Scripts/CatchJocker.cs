using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatchJocker : MonoBehaviour
{
    private Policeman role;
    private List<Jocker> enemy;
    [SerializeField]
    [Range(0, 10)]
    private float catchDistance = 5f;

    public UnityAction<Jocker> OnJockerCaught;

    // Start is called before the first frame update
    void Start()
    {
        
        role = GetComponent<Policeman>();
        enemy = new List<Jocker>(FindObjectsOfType<Jocker>());

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var jocker in enemy)
        {
            if (jocker.wasArrested)
            {
                continue;
            }
            if (Vector3.Distance(transform.position, jocker.transform.position) < catchDistance)
            {
                jocker.wasArrested = true;
                jocker.gameObject.SetActive(false);
                //Destroy(jocker.gameObject);
                OnJockerCaught?.Invoke(jocker);
            }
        }
    }
}

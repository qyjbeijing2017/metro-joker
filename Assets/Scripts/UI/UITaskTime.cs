using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITaskTime : MonoBehaviour
{
    [SerializeField]
    List<Slider> sliders = new List<Slider>();
    List<Task> tasks;
    
    // Start is called before the first frame update
    void Start()
    {
        tasks = new List<Task>(FindObjectsOfType<Task>());
        for (int i = 0; i < tasks.Count; i++)
        {
            sliders[i].transform.position = Camera.main.WorldToScreenPoint(tasks[i].transform.position + Vector3.up * 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            if(tasks[i].isRunning){
                sliders[i].gameObject.SetActive(true);
                sliders[i].value = tasks[i].arriveTime / tasks[i].arriveTime2Finished;
            } else {
                sliders[i].gameObject.SetActive(false);
            }
        }
        for (int i = tasks.Count; i < sliders.Count; i++)
        {
            sliders[i].gameObject.SetActive(false);
        }
    }
}

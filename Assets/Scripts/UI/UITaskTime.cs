using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITaskTime : MonoBehaviour
{
    [SerializeField]
    List<TextMeshPro> textMeshPros = new List<TextMeshPro>();
    List<Task> tasks;
    
    // Start is called before the first frame update
    void Start()
    {
        tasks = new List<Task>(FindObjectsOfType<Task>());
    }

    // Update is called once per frame
    void Update()
    {
        var index = 0;
        foreach(Task task in tasks)
        {
            if(!task.isRunning) {
                textMeshPros[index].text = task.arriveTime / task.arriveTime2Finished + "%";
                textMeshPros[index].color = task.taskColor;
                textMeshPros[index].enabled = true;
                index++;
                continue;
            }
        }
        for(; index < textMeshPros.Count; index++)
        {
            textMeshPros[index].enabled = false;
        }
    }
}

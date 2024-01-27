using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Line))]
public class LineShow : MonoBehaviour
{
    LineRenderer lineRenderer;
    Line line;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        line = GetComponent<Line>();
        lineRenderer.positionCount = line.stations.Count;
        for(int i = 0; i < line.stations.Count; i++) {
            lineRenderer.SetPosition(i, line.stations[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

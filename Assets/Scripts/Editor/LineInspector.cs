using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(Line))]
public class LineInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        // add button
        if (GUILayout.Button("填充站点数据和路径"))
        {
            var stations = GameObject.FindObjectsOfType<Station>();
            foreach (var s in stations)
            {
                s.lines.Clear();
            }

            var lines = FindObjectsOfType<Line>();
            foreach (var l in lines)
            {
                GenData(l);
            }

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }

    private void GenData(Line l)
    {
        foreach (var s in l.stations)
        {
            s.AddLine(l);
        }

        foreach (var t in l.trains)
        {
            ObjectPool.Push("train", t);
        }

        l.trains.Clear();
        var distanceBetweenTrains = l.timeGap * l.trainSpeed;
        // add all distances between stations
        var totalDistance = 0f;
        for (var i = 0; i < l.stations.Count - 1; i++)
        {
            var s1 = l.stations[i];
            var s2 = l.stations[i + 1];
            var distance = Vector3.Distance(s1.transform.position, s2.transform.position);
            totalDistance += distance;
        }

        var count = Mathf.FloorToInt(totalDistance / distanceBetweenTrains);
        for (int i = 0; i < count; i++)
        {
            var t = ObjectPool.Pop<Train>("train");
            t.Spawn(l, new MoveState
            {
                line = l,
                station = l.stations[0],
                reverse = false,
            });
            t.MockDistance(i * distanceBetweenTrains, false);
        }

        for (int i = 0; i < count; i++)
        {
            var t = ObjectPool.Pop<Train>("train");
            t.Spawn(l, new MoveState
            {
                line = l,
                station = l.stations[^1],
                reverse = true,
            });
            t.MockDistance(i * distanceBetweenTrains, true);
        }
    }
}
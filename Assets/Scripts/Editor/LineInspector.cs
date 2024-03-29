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
            // ObjectPool.Collect<Train>("train");
            ObjectPool.Reset();

            var cars = GameObject.FindObjectsOfType<Train>();
            foreach (var c in cars)
            {
                Destroy(c.gameObject);
            }

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
        l.material = l.GetComponent<LineRenderer>().sharedMaterial;
        foreach (var s in l.stations)
        {
            s.AddLine(l);
        }

        var distanceBetweenTrains = l.timeGap * l.trainSpeed;
        // add all distances between stations
        var totalDistance = 0f;
        Station[] stationArr;
        if (l.isRing)
        {
            stationArr = new Station[l.stations.Count + 1];
            l.stations.CopyTo(stationArr);
            stationArr[^1] = stationArr[0];
        }
        else
        {
            stationArr = l.stations.ToArray();
        }

        for (var i = 0; i < stationArr.Length - 1; i++)
        {
            var s1 = stationArr[i];
            var s2 = stationArr[i + 1];
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
            t.MockDistance(i * distanceBetweenTrains, stationArr, false);
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
            t.MockDistance(i * distanceBetweenTrains, stationArr, true);
        }
    }
}
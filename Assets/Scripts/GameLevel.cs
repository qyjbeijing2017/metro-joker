using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private Line[] lines;
    private List<IRoleBase> jokers = new();
    private List<IRoleBase> police = new();
    private float captureDistance = 1;
    private GameModeBase mode;

    public void StartGame()
    {
        TimeManager.SetTimeScale(1);
        TimeManager.AddScaledTick(Tick);
    }

    public void StopGame()
    {
        TimeManager.SetTimeScale(0);
        TimeManager.AddScaledTick(Tick);
    }

    private void Tick(float dt)
    {
        CheckCapture();
        CheckResult();
        mode.Tick(dt);
    }

    private void CheckCapture()
    {
        foreach (var j in jokers)
        {
            var jPos = j.t.position;
            var ps = police.Where(i => Vector3.Distance(i.t.position, jPos) < captureDistance).ToList();
            if (ps.Count <= 0)
                continue;
            Capture(ps, j);
        }
    }

    private void Capture(List<IRoleBase> police, IRoleBase joker)
    {
    }

    private void CheckResult()
    {
    }
}
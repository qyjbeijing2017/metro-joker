using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private Line[] lines;

    public void StartGame()
    {
        foreach (var line in lines)
        {
            line.Init();
        }
    }

    public void StopGame()
    {
    }
}
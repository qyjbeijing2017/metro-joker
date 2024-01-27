using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
    Station station;
    [SerializeField] private string TaskName = "Task";

    public string taskName
    {
        get { return TaskName; }
    }

    [SerializeField] private Color TaskColor = Color.white;

    public Color taskColor
    {
        get { return TaskColor; }
    }

    [SerializeField] private float ArriveTime2Finished = 5.0f;

    public float arriveTime2Finished
    {
        get { return ArriveTime2Finished; }
    }

    private float ArriveTime = 0.0f;

    public float arriveTime
    {
        get { return ArriveTime; }
    }

    public bool isRunning { get; private set; } = false;

    public bool isFinished
    {
        get { return ArriveTime >= ArriveTime2Finished; }
    }

    public UnityAction OnTaskFinished;


    // Start is called before the first frame update
    void Start()
    {
        station = GetComponent<Station>();
        TimeManager.AddScaledTick(OnTick);
    }

    private float deltaTime = 0;

    void OnTick(float deltaTime)
    {
        this.deltaTime = deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished) return;
        foreach (IRoleBase role in station.roles)
        {
            if (role is Jocker)
            {
                ArriveTime += deltaTime;
                isRunning = true;
                if (isFinished)
                {
                    OnTaskFinished?.Invoke();
                    isRunning = false;
                }

                return;
            }
        }

        ArriveTime = 0;
        isRunning = false;
    }

    void OnDestroy()
    {
        TimeManager.RemoveScaledTick(OnTick);
    }
}
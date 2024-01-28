using UnityEngine;

public class SlowDown : Skill
{
    Policeman policeman;

    [SerializeField]
    int slowDownTimes = 3;
    [SerializeField]
    float slowDownMultiplier = 0.5f;

    int slowDownCount = 0;
    protected override string vidName => "poison";

    public override bool UseSkill()
    {

        if(policeman.train)
        {
            Debug.Log("use slow down");
            policeman.train.line.SetSpeedMultiplier(slowDownMultiplier);
            policeman.train.onReachStation.AddListener(onDock);
            slowDownCount = 0;
            return true;
        }
        return false;
    }

    void onDock(bool isTerminal) {
        Debug.Log("onDock");
        if(isTerminal) {
            slowDownCount = 0;
            policeman.train.line.SetSpeedMultiplier(1f);
            policeman.train.onReachStation.RemoveListener(onDock);
            Debug.Log("onTerminal");
        } else {
            slowDownCount++;
            if(slowDownCount >= slowDownTimes) {
                policeman.train.line.SetSpeedMultiplier(1f);
                policeman.train.onReachStation.RemoveListener(onDock);
                Debug.Log("OnSlowDownEnd");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        policeman = GetComponent<Policeman>();
    }

    void OnDestroy() {
        //policeman.train.onReachStation.RemoveListener(onDock);
    }
}

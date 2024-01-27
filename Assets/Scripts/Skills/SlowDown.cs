using UnityEngine;

public class SlowDown : Skill
{
    Policeman policeman;

    [SerializeField]
    int slowDownTimes = 3;
    [SerializeField]
    float slowDownMultiplier = 0.5f;

    int slowDownCount = 0;


    public override bool UseSkill()
    {

        if(policeman.train)
        {
            Debug.Log("¼õËÙ");
            policeman.train.line.SetSpeedMultiplier(slowDownMultiplier);
            policeman.train.onReachStation.AddListener(onDock);
            slowDownCount = 0;
            return true;
        }
        return false;
    }

    void onDock(bool isTerminal) {
        Debug.Log("½øÕ¾");
        if(isTerminal) {
            slowDownCount = 0;
            policeman.train.line.SetSpeedMultiplier(1f);
            policeman.train.onReachStation.RemoveListener(onDock);
            Debug.Log("»Ö¸´");
        } else {
            slowDownCount++;
            if(slowDownCount >= slowDownTimes) {
                policeman.train.line.SetSpeedMultiplier(1f);
                policeman.train.onReachStation.RemoveListener(onDock);
                Debug.Log("»Ö¸´");
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

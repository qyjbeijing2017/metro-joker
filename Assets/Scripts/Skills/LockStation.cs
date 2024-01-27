using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockStation : Skill
{
    Jocker jocker;
    [SerializeField]
    float lockTime = 5f;

    float lockTimeLeft = 0f;

    public override bool UseSkill()
    {
        if(!jocker.train && !jocker.current.station.isTerminal) {
            jocker.current.station.SetStuck(true);
            lockTimeLeft = lockTime;
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        jocker = GetComponent<Jocker>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lockTimeLeft > 0f) {
            lockTimeLeft -= deltaTime;
            if(lockTimeLeft <= 0f) {
                jocker.current.station.SetStuck(false);
            }
        }
        
    }
}

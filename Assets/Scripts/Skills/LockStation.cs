using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockStation : Skill
{
    Joker _joker;
    [SerializeField]
    float lockTime = 5f;

    float lockTimeLeft = 0f;

    public override bool UseSkill()
    {
        if(!_joker.train && !_joker.station.IsOnlyTerminal()) {
            _joker.station.SetStuck(true);
            lockTimeLeft = lockTime;
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _joker = GetComponent<Joker>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lockTimeLeft > 0f) {
            lockTimeLeft -= deltaTime;
            if(lockTimeLeft <= 0f) {
                _joker.station.SetStuck(false);
            }
        }
        
    }
}

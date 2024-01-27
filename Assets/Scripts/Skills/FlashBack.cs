using System.Collections;
using System.Collections.Generic;
using Roles;
using UnityEngine;

public class FlashBack : Skill
{
    [SerializeField]
    private PoliceBeacon policeBeacon;
    private Policeman policeman;


    void Start()
    {
        policeman = GetComponent<Policeman>();
        policeBeacon = Instantiate(policeBeacon, transform.position, Quaternion.identity);
        policeBeacon.enabled = false;
    }

    public override bool UseSkill()
    {
        // Todo: flash back
        if (policeman.train)
        {
            if(policeBeacon.enabled == true) {
                // policeman.GetOff(policeman.train);

            }
            return true;
        }
        else 
        {
            // policeBeacon.Set(policeman.train.station, policeman.train.roleId);
            policeBeacon.enabled = true;
            return false;
        }
    }
}

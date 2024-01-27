using UnityEngine;

public class FlashBack : Skill
{
    [SerializeField]
    private PoliceBeacon policeBeacon;
    private Policeman policeman;
    private int roleId;


    void Start()
    {
        policeman = GetComponent<Policeman>();
        policeBeacon = Instantiate(policeBeacon, transform.position, Quaternion.identity);
        policeBeacon.enabled = false;
        roleId = GetComponent<InputManager>().playerID;
    }

    public override bool UseSkill()
    {
        // Todo: flash back
        if (policeman.train)
        {
            if(policeBeacon.enabled == true) {
                // // Todo: flash back
                // policeman.current.station = policeBeacon.station;

                // policeBeacon.Set(policeman.current.station, roleId);
                (policeman as IRoleBase).GetOff(policeBeacon.station);
                policeBeacon.enabled = false;
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

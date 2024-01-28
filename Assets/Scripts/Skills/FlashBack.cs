using UnityEngine;

public class FlashBack : Skill
{
    [SerializeField]
    private PoliceBeacon policeBeacon;
    private Policeman policeman;
    private int roleId;
    protected override string vidName => "tele";


    protected void Start()
    {
        base.Start();
        policeman = GetComponent<Policeman>();
        policeBeacon = Instantiate(policeBeacon, transform.position, Quaternion.identity);
        policeBeacon.gameObject.SetActive(false);
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
                (policeman as IRoleBase).EnterStation(policeBeacon.station);
                policeBeacon.gameObject.SetActive(false);
            }
            return true;
        }
        else 
        {
            policeBeacon.station = policeman.station;
            policeBeacon.transform.position = policeman.transform.position;
            policeBeacon.gameObject.SetActive(true);
            return false;
        }
    }
}

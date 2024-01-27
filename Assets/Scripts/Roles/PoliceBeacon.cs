using UnityEngine;

public class PoliceBeacon : MonoBehaviour
{
    private Station station;
    
    private int roleId;

    public void Set(Station station, int roleId)
    {
        this.station = station;
        this.roleId = roleId;

        // set style
    }
}
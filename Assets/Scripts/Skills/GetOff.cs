using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOff : Skill
{
    [SerializeField]
    [Range(0, 100)]
    private float getOffDistance = 10f;

    private Jocker[] jockers;

    void Start()
    {
        jockers = FindObjectsByType<Jocker>(FindObjectsSortMode.None);
    }

    public override bool UseSkill()
    {

        foreach (Jocker jocker in jockers)
        {
            if (jocker == null) continue;
            if (jocker.gameObject == gameObject) continue;
            if (Vector3.Distance(transform.position, jocker.transform.position) < getOffDistance)
            {
                jocker.next.stay = true;
            }
        }
        return true;
    }

}

using UnityEngine;

public class GetOff : Skill
{
    [SerializeField] [Range(0, 100)] private float getOffDistance = 10f;

    private Policeman[] polices;
    protected override string vidName => "off";

    void Start()
    {
        base.Start();
        polices = FindObjectsByType<Policeman>(FindObjectsSortMode.None);
    }

    public override bool UseSkill()
    {
        foreach (Policeman police in polices)
        {
            if (police == null) continue;
            if (police.gameObject == gameObject) continue;
            if (Vector3.Distance(transform.position, police.transform.position) < getOffDistance)
            {
                police.willStay = true;
            }
        }

        return true;
    }
}
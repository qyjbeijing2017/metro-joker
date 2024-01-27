using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    InputManager inputManager;
    [SerializeField]
    float CoolDown = 10f;
    float CurrentCoolDown = 0f;
    public float currentCoolDown
    {
        get
        {
            return CurrentCoolDown;
        }
    }
    public float maxCoolDown
    {
        get
        {
            return CoolDown;
        }
    }

    protected float deltaTime = 0f;

    protected void Start()
    {
        
        inputManager = GetComponent<InputManager>();
        TimeManager.AddScaledTick(OnTick);
    }
    void OnTick(float dt)
    {
        deltaTime = dt;
    }
    void Update()
    {

        if (inputManager.Skill && currentCoolDown <= 0f)
        {
            if(UseSkill())
            {
                CurrentCoolDown = CoolDown;
            }
        } else if (currentCoolDown > 0f)
        {
            CurrentCoolDown -= deltaTime;
        }
    }

    public abstract bool UseSkill();

    void OnDestroy()
    {
        TimeManager.RemoveScaledTick(OnTick);
    }
}

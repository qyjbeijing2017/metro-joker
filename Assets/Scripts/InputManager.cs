using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private int PlayerID;

    public int playerID {
        get {
            return PlayerID;
        }
    }

    [SerializeField]
    private GamepadButton GetOffButton;
    [SerializeField]
    private GamepadButton SkillButton;

    public Vector2 move{
        get{
            if(PlayerID + 1 > Gamepad.all.Count)
            {
                return Vector2.zero;
            }
            return Gamepad.all[PlayerID].leftStick.ReadValue();
        }
    }

    public Vector2 direction{
        get{
            if(PlayerID + 1 > Gamepad.all.Count)
            {
                return Vector2.zero;
            }
            return Gamepad.all[PlayerID].rightStick.ReadValue();
        }
    }

    public bool getOff{
        get{
            if(PlayerID + 1 > Gamepad.all.Count)
            {
                return false;
            }
            return Gamepad.all[PlayerID][GetOffButton].wasPressedThisFrame;
        }
    }

    public bool Skill{
        get{
            if(PlayerID + 1 > Gamepad.all.Count)
            {
                return false;
            }
            return Gamepad.all[PlayerID][SkillButton].wasPressedThisFrame;
        }
    }


    void Update()
    {
        if(getOff) {
            Debug.Log("GetOff");
        }
        if(Skill) {
            Debug.Log("Skill");
        }
        if(move != Vector2.zero) {
            Debug.Log("Move");
        }
        if(direction != Vector2.zero) {
            Debug.Log("Direction");
        }

    }
}

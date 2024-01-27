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
    private GamepadButton GetOnAxis;

    [SerializeField]
    private GamepadButton GetOffButton;
    [SerializeField]
    private GamepadButton SkillButton;

    public Vector2 direction{
        get{
            if(PlayerID + 1 > Gamepad.all.Count)
            {
                return Vector2.zero;
            }
            if(GetOnAxis == GamepadButton.LeftStick)
            return Gamepad.all[PlayerID].leftStick.ReadValue();
            else if(GetOnAxis == GamepadButton.RightStick)
            return Gamepad.all[PlayerID].rightStick.ReadValue();
            else
            return Vector2.zero;
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

    }
}

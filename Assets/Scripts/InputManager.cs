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

    [Space(10)]
    [SerializeField]
    bool useKeyBoard;
    [SerializeField]
    private Key up;
    [SerializeField]
    private Key down;
    [SerializeField]
    private Key left;
    [SerializeField]
    private Key right;

    [SerializeField]
    private Key getoff;
    [SerializeField]
    private Key skill;

    public Vector2 direction{
        get{
            if(useKeyBoard) {
                Vector2 dir = Vector2.zero;
                if (Keyboard.current[up].isPressed)
                {
                    dir.y = 1f;
                }
                if (Keyboard.current[down].isPressed)
                {
                    dir.y = -1f;
                }
                if (Keyboard.current[left].isPressed)
                {
                    dir.x = -1f;
                }
                if (Keyboard.current[right].isPressed)
                {
                    dir.x = 1f;
                }
                return dir;
            }


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

            if(useKeyBoard) {
                return Keyboard.current[getoff].wasPressedThisFrame;
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

            if(useKeyBoard) {
                return Keyboard.current[skill].wasPressedThisFrame;
            }
            
            return Gamepad.all[PlayerID][SkillButton].wasPressedThisFrame;
        }
    }


    void Update()
    {

    }
}

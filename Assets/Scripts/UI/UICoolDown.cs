using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UICoolDown : MonoBehaviour
{
    [SerializeField]
    private int PlayerID;
    Image image;
    Skill skill;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        var inputs = FindObjectsByType<InputManager>(FindObjectsSortMode.None);
        foreach (var input in inputs)
        {
            if (input.playerID == PlayerID)
            {
                skill = input.GetComponent<Skill>();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = skill.currentCoolDown / skill.maxCoolDown;
    }
}

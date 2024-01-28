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
    [SerializeField]
    Image image;
    Skill skill;

    Jocker jocker;
    // Start is called before the first frame update
    void Start()
    {
        var inputs = FindObjectsByType<InputManager>(FindObjectsSortMode.None);
        foreach (var input in inputs)
        {
            if (input.playerID == PlayerID)
            {
                skill = input.GetComponent<Skill>();
                jocker = input.GetComponent<Jocker>();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(skill == null)
        {
            return;
        }
        image.fillAmount = skill.currentCoolDown / skill.maxCoolDown;
        if(jocker && jocker.wasArrested) {
            image.fillAmount = 1f;
        }
    }
}
 
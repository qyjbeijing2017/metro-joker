using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void OnStart(){
        SceneManager.LoadScene("SampleScene");
    }

    public void OnAbout(){
        SceneManager.LoadScene("About");
    }

    public void OnExit(){
        Application.Quit();
    }
}

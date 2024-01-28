using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void OnStart(){
        SceneManager.LoadScene("20240127");
    }

    public void OnAbout(){
        SceneManager.LoadScene("About");
    }

    public void OnExit(){
        Application.Quit();
    }
}

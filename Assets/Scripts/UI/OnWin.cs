using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnWin : MonoBehaviour
{
    [SerializeField]
    RawImage policeWin;
    [SerializeField]
    RawImage jockerWin;
    [SerializeField]
    Button back;
    GamePlay gamePlay;
    // Start is called before the first frame update
    void Start()
    {
        gamePlay = FindObjectOfType<GamePlay>();
        policeWin.gameObject.SetActive(false);
        jockerWin.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
        gamePlay.OnGameFinished += OnGameFinished;
    }

    void OnGameFinished(bool jockerWin)
    {
        Debug.Log("Game Finished!!!");
        if (jockerWin)
        {
            this.jockerWin.gameObject.SetActive(true);
        }
        else
        {
            this.policeWin.gameObject.SetActive(true);
        }
        back.gameObject.SetActive(true);
    }

    public void OnBack() {
        SceneManager.LoadScene("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

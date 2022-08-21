using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winScreenUI;
    public Text strokeText;
    public BallControl ballControlObject;

    public void CourseComplete ()
    {
        winScreenUI.SetActive(true);
        strokeText.text = "Number of Strokes: " + ballControlObject.score;
       
        Invoke("LoadNextScene", 1f);
    }

    void Update()
    {
        /*
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        */
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

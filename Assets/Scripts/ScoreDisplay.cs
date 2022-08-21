using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    public BallControl ballControl;
    public Text displayText;

    void Awake()
    {
        //displayText.color = new Color(0.972f, 0.698f, 0.329f);
    }

    // Update is called once per frame
    void Update()
    {
        displayText.text = ballControl.score.ToString();
    }
}

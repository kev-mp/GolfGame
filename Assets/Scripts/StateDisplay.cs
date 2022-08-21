using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateDisplay : MonoBehaviour
{
    public BallControl ballControl;
    public Text displayText;

    // Update is called once per frame
    void Update()
    {
        if (!ballControl.isIdle)
        {
            displayText.text = "Ball is moving...";
            displayText.color = new Color(0.968f, 0.403f, 0.392f);
        } else
        {
            if (ballControl.isAiming)
            {
                displayText.text = "Aiming in progress.";
                displayText.color = new Color(0.972f, 0.698f, 0.329f);
            } else
            {
                displayText.text = "Ball is ready to putt.";
                displayText.color = new Color(0.486f, 0.909f, 0.450f);
            }
        }
    }
}

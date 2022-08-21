using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    float xRotation = 0f;
    float yRotation = 0f;

    public bool isIdle;
    public bool launchBuffer;
    public bool isAiming;
    public int score;

    private float mouseYDelta;
    private float mouseYMax = 60f;
    public float shootForce = 3f;
    private Vector3 lastLocation;

    public Rigidbody ball;
    public LineRenderer line;

    public Image powerBarImage;

    public float rotSpeed = 5f;

    /**
     * Initializes some values on startup
     */
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        ball.maxAngularVelocity = 50f;

        isIdle = true;
        launchBuffer = false;
        isAiming = false;
        score = 0;
        line.gameObject.SetActive(false);
    }

    /**
     * Method to update game as soon as possible
     */
    void Update ()
    {
        if (!PauseMenu.gamePaused) {
            //Sets idle state once ball is stable
            if (ball.velocity.magnitude <= 0f)
            {
                isIdle = true;
                lastLocation = ball.position;
            }

            //Releases the ball and starts the launchBuffer boolean
            if (Input.GetMouseButtonUp(0) && isAiming)
            {
                if (mouseYDelta > 0.01)
                {
                    launchBuffer = true;
                }
                else
                {
                    isAiming = false;
                    isIdle = true;
                }
            
            }
        
            //Allows aiming once idle
            if (Input.GetMouseButtonDown(0) && isIdle)
            {
                isAiming = true;
                mouseYDelta = 0;
            }

            //Cancels aiming using the right-mouse button
            if(Input.GetMouseButtonDown(1) && isAiming)
            {
                isAiming = false;
                line.gameObject.SetActive(false);
                mouseYDelta = 0f;
            }

            //Determines the order of events depending on state
            if (isAiming && isIdle)
            {
                Aim();
                DrawLine();
            
            } else
            {
                Look();

                if (Input.GetKeyDown(KeyCode.R))
                {
                    ball.position = lastLocation;
                    ball.velocity = new Vector3(0f, 0f, 0f);
                    ball.angularVelocity = new Vector3(0f, 0f, 0f);
                }

            }
            DrawPowerBar();

        }
    }

    /**
     * Method to update game at a fixed rate (used for physics)
     */
    void FixedUpdate ()
    {
        Launch();
    }

    
    /*
     * Determines camera movement
     */
    private void Look()
    {
        transform.position = ball.position;

        xRotation += Input.GetAxis("Mouse X") * rotSpeed;
        yRotation += Input.GetAxis("Mouse Y") * rotSpeed;
        if (yRotation < -70f)
        {
            yRotation = -70f;
        }

        if (yRotation > 20)
        {
            yRotation = 20;
        }
        transform.rotation = Quaternion.Euler(-yRotation, xRotation, 0f);

    }

    /**
     * Calculates aim values based on mouse movement along the Y-axis
     */
    private void Aim()
    {
        mouseYDelta += Input.GetAxis("Mouse Y");

        if(mouseYDelta > mouseYMax)
        {
            mouseYDelta = mouseYMax;
        } else if (mouseYDelta <= 0f)
        {
            mouseYDelta = 0f;
        }
        //Debug.Log(mouseYDelta);
    }

    /**
     * Method to apply force to ball once player "hits" it
     */
    private void Launch()
    {
        
        if (!isIdle)
        {
            //launchBuffer = false;
            return;
        }

        if (launchBuffer)
        {
            Vector3 nonYForward = new Vector3(transform.forward.x, 0, transform.forward.z);
            ball.AddForce(nonYForward.x * shootForce * mouseYDelta, nonYForward.y, nonYForward.z * shootForce * mouseYDelta);
            //ball.velocity = nonYForward * shootForce * mouseYDelta;
            isIdle = false;
            launchBuffer = false;
            isAiming = false;
            score = score + 1;
            line.gameObject.SetActive(false);

            Debug.Log("MouseY moved " + mouseYDelta + " pixels while button was down.");
            mouseYDelta = 0f;


        }

    }
    
    /**
     *  Draws aim line with appropriate analog length
     */
    private void DrawLine()
    {
        Vector3 forwardNoY = new Vector3(transform.forward.x, 0, transform.forward.z);
        line.gameObject.SetActive(true);
        line.SetPosition(0, ball.position);
        line.SetPosition(1, ball.position + forwardNoY.normalized * mouseYDelta * 0.1f);
        
    }

    private void DrawPowerBar()
    {
        powerBarImage.fillAmount = Mathf.Clamp(mouseYDelta / mouseYMax, 0, 1f);
    }
}

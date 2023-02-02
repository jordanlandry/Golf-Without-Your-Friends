using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallHitScript : MonoBehaviour
{
    // Variables for checking if it's in the hole
    public Transform holeCheck;
    public LayerMask holeMask;
    bool isInTheHole;
    public GameObject ball;
    float ballSize;
    float timeInHole = 0;

    private int nextSceneToLoad;

    public Rigidbody playerRB;
    public bool shooting = false;
    public bool isMoving = false;
    public float ShootAngle { get; protected set; }

    // Variables For Score
    public int HolePar;
    private int playerScore;
    private int strokes;
    private int strokesToString;
    public TextMeshProUGUI txt_StrokeCounter;
    public TextMeshProUGUI txtHolePar;
    public TextMeshProUGUI txtScore;
    string stringScore;
    public static int totalScore;


    // Variables for Power
    public Vector3 power;
    public float maxPower = 20f;
    private float powerToFloat = 0f;
    public float powerPercent { get { return powerToFloat / maxPower; } }

    // Mouse Inputs
    float startPos;
    float endPos;

    // Variables for Camera Look
    private float camX;
    private float camY;
    private Vector3 cameraRotateValue;
    public Transform camTransform;
    bool verticalCamLock;

    // Variables for out of bounds logic
    Vector3 previousBallPos;
    bool isOutOfBounds = false;
    public LayerMask outOfBoundsMask;
    float outOfBoundsTime;

    // Sound Handler
    private SoundHandler sh;
    bool soundPlayed;
    bool nextShotSoundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        txtScore.enabled = false;
        soundPlayed = false;
        Cursor.lockState = CursorLockMode.Locked;
        strokes = 0;
        playerRB.sleepThreshold = 0.05f; // Default is 0.005
        txtHolePar.text = "Par " + HolePar;
        ballSize = ball.transform.localScale.x;
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;

        sh = GetComponent<SoundHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // Exit on Escape press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        power = new Vector3(0, 0, powerToFloat);
        isMoving = !playerRB.IsSleeping();
        if (Input.GetKeyUp(KeyCode.Mouse0) && !isMoving)    // When you let go of the mouse, it will activate the shooting 
        {
            shooting = true;
        }

        if (Input.GetKey(KeyCode.Mouse0) && !isMoving)      // When you are holding Left Click
        {
            verticalCamLock = true;
            calculatePower();
        }
        else verticalCamLock = false;
        ShootAngle = camY;
        cameraRotate();
        readyToShoot();

        // Out of Bounds
        isOutOfBounds = Physics.CheckSphere(holeCheck.position, ballSize, outOfBoundsMask);
        if (isOutOfBounds)
            outOfBoundsTime += 100 * Time.deltaTime;
        if (isOutOfBounds && outOfBoundsTime > 100)
            outOfBounds();

        isInTheHole = Physics.CheckSphere(holeCheck.position, ballSize, holeMask);
        if (isInTheHole)
            inTheHole();
    }

    void FixedUpdate()
    {
        if (shooting && !isMoving)
        {
            Shoot(power);
        }
    }

    void Shoot(Vector3 power)
    {
        previousBallPos = ball.transform.localPosition;
        if (powerToFloat <= 0)      // If the player did not take a shot, return out of the method
        {
            resetVariables();
            return;
        }
        sh.playShootSound();    // Has to play the sound first so that the sound can use powerPercentage variable

        playerRB.AddForce(Quaternion.Euler(0, ShootAngle, 0) * power, ForceMode.Impulse);
        shooting = false;
        updateStroke();
        resetVariables();
    }

    void cameraRotate()
    {
        camY += Input.GetAxis("Mouse X");
        if (verticalCamLock)
        {
            camTransform.transform.rotation = Quaternion.Euler(camX, camY, 0);
        }
        else
        {
            camX -= Input.GetAxis("Mouse Y");
            camX = Mathf.Clamp(camX, -20f, 70f);
            camTransform.transform.rotation = Quaternion.Euler(camX, camY, 0);
        }
    }

    void updateStroke()
    {
        string strStrokes = " Strokes";
        strokes++;
        if (strokes == 1) strStrokes = " Stroke";
        txt_StrokeCounter.text = strokes + strStrokes;
    }

    void calculatePower()
    {
        startPos = Input.GetAxis("Mouse Y");
        endPos += Input.GetAxis("Mouse Y");

        powerToFloat = endPos - startPos;
        if (powerToFloat > maxPower)
            powerToFloat = maxPower;
        if (powerToFloat < 0)
            powerToFloat = 0;
    }

    void resetVariables()
    {
        startPos = 0;
        endPos = 0;
        powerToFloat = 0;
        shooting = false;
        nextShotSoundPlayed = false;
    }

    void inTheHole()
    {
        calculateScore();

        timeInHole += Time.deltaTime;
        if (timeInHole >= 1)
            if (nextSceneToLoad >= SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(0);
                nextSceneToLoad = 0;
                Cursor.lockState = CursorLockMode.None;
            }
            else SceneManager.LoadScene(nextSceneToLoad);

        if (!soundPlayed)
            sh.playInTheHoleSound();
        soundPlayed = true;

    }

    void calculateScore()
    {
        playerScore = strokes - HolePar;

        // This is to set up the text that pops up on the screen when you get it in.
        if (playerScore < -3)
            stringScore = (-1 * playerScore) + " Under Par";
        if (playerScore == -3)
            stringScore = "ALBATROSS";
        if (playerScore == -2)
            stringScore = "EAGLE";
        if (playerScore == -1)
            stringScore = "BIRDIE";
        if (playerScore == 0)
            stringScore = "PAR";
        if (playerScore == 1)
            stringScore = "BOGEY";
        if (playerScore == 2)
            stringScore = "DOUBLE BOGEY";
        if (playerScore == 3)
            stringScore = "TRIPLE BOGEY";
        if (playerScore > 3)
            stringScore = playerScore + " Over Par";

        if (strokes == 1)
            stringScore = "HOLE IN ONE!";

        txtScore.enabled = true;
        txtScore.text = stringScore;
    }

    void outOfBounds()
    {
        outOfBoundsTime = 0;
        playerRB.constraints = RigidbodyConstraints.FreezePosition;         // This will freeze the ball in place
        resetVariables();
        Debug.Log(outOfBoundsTime);
        ball.transform.position = previousBallPos + new Vector3(0, .5f, 0);
        playerRB.constraints = RigidbodyConstraints.None;
    }

    void readyToShoot()
    {
        if (!isMoving && !shooting && Input.GetKeyDown(KeyCode.Mouse0) != true && !nextShotSoundPlayed && strokes != 0 && !isInTheHole)
        {
            sh.playReadyToShootSound();
            nextShotSoundPlayed = true;
        }

    }


}
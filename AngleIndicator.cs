using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleIndicator : MonoBehaviour
{
    public Transform Arrow_DEFAULT;
    private Vector3 defaultSize;
    // Start is called before the first frame update
    void Start()
    {
        ballHit = GameObject.FindObjectOfType<BallHitScript>();
        defaultSize = new Vector3(1f, 1f, 1f);
    }
    BallHitScript ballHit;

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, ballHit.ShootAngle, 0);
        if (ballHit.isMoving)  // This will Hide the arrow when your ball is moving
            transform.localScale = new Vector3(0,0,0);
            else transform.localScale = defaultSize;
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !ballHit.isMoving)
            Arrow_DEFAULT.transform.localScale = new Vector3(0,0,0);
        if (!ballHit.shooting)
            Arrow_DEFAULT.transform.localScale = defaultSize;
    }
}

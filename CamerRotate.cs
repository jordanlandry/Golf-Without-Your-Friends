using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerRotate : MonoBehaviour
{
    private float pivotAngle;
    public float pivotSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pivotAngle = 0;
        pivotAngle += pivotSpeed * Time.deltaTime;
        transform.Rotate(0, pivotAngle, 0);
    }
}

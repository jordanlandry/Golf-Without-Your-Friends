using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProperties : MonoBehaviour
{
    private float scrollOffset;
    public float maxFOV;
    public float minFOV;
    public float scrollSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)     // Scrolling down / zooming out
        {
            scrollOffset = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            Camera.main.fieldOfView -= scrollOffset;

            if (Camera.main.fieldOfView > maxFOV)
                Camera.main.fieldOfView = maxFOV;
            if (Camera.main.fieldOfView < minFOV)
                Camera.main.fieldOfView = minFOV;
        }     
    }
}

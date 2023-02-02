using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBallColor : MonoBehaviour
{

    public Renderer ball;
    public Color[] color;
    static int colorIndex;

    void Start() 
    {
        ball.material.SetColor("_Color", color[colorIndex]);
        Debug.Log(colorIndex);
    }

    public void nextColor()
    {
        colorIndex++;
        if (colorIndex >= color.Length)
            colorIndex = 0;
        ball.material.SetColor("_Color", color[colorIndex]);
    }

    public void previousColor() 
    {
        colorIndex--;
        if (colorIndex < 0) 
            colorIndex = color.Length-1;
        ball.material.SetColor("_Color", color[colorIndex]);
    }
}

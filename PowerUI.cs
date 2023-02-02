using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour
{
    BallHitScript ballHit;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        ballHit = GameObject.FindObjectOfType<BallHitScript>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = ballHit.powerPercent;
    }
}

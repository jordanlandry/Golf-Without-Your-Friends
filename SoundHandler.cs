using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    BallHitScript ballHit;
    public AudioSource[] shootSounds;
    public AudioSource[] inTheHoleSounds;
    public AudioSource selectItemSound;
    public AudioSource readyToShootSound;
    
    int shootLength;
    int inTheHoleLength;
    int randomNumber;


    // Start is called before the first frame update
    void Start()
    {
        ballHit = GameObject.FindObjectOfType<BallHitScript>();
        shootLength = shootSounds.Length;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playShootSound() 
    {
        randomNumber = Random.Range(0, shootLength);
        shootSounds[randomNumber].volume = ballHit.powerPercent;
        if(ballHit.powerPercent < .5f)
            shootSounds[randomNumber].volume = 0.5f;
        Debug.Log(ballHit.powerPercent);
        shootSounds[randomNumber].Play();
    }

    public void playInTheHoleSound()
    {
        randomNumber = Random.Range(0, inTheHoleLength);
        inTheHoleSounds[randomNumber].Play();
    }

    public void playSelectItemSound() 
    {
        selectItemSound.Play();
    }

    public void playReadyToShootSound()
    {
        readyToShootSound.Play();
    }
}

using UnityEngine;

public class FollowBall : MonoBehaviour
{

    public Transform player;
    public Vector3 cameraOffset;
    
    void Start() 
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + cameraOffset;
    }
}

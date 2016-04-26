using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour {

    public GameObject catapultBall;
    public Vector2 ballForce;
    public bool hasFired = false;

    private Rigidbody2D ballBody;

    void Awake()
    {
        ballBody = catapultBall.GetComponent<Rigidbody2D>();
    }

    public void LaunchBall()
    {
        hasFired = true;
        ballBody.AddForce(ballForce);
    }

    

}

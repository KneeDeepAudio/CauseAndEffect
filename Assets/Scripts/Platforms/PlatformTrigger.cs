using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour {

    public MovingPlatfrm platform;

    void OnCollisionEnter2D(Collision2D collider)
    {
        if ((collider.gameObject.tag == "Block" ||
            collider.gameObject.tag == "StartBlock" ||
            collider.gameObject.tag == "Bolt" ||
            collider.gameObject.tag == "CatapultBall") && GameManager.instance.inPlay)
        {
            Debug.Log("Arrow Fired");
            platform.isTriggered = false;
        }
    }
}

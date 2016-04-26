﻿using UnityEngine;
using System.Collections;

public class CatapultTrigger : MonoBehaviour {

    public Catapult catapult;

    void OnCollisionEnter2D(Collision2D collider)
    {
        if ((collider.gameObject.tag == "Block" || collider.gameObject.tag == "StartBlock") && catapult.hasFired == false)
        {
            catapult.LaunchBall();
        }
    }

}
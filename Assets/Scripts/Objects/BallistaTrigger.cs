using UnityEngine;
using System.Collections;

public class BallistaTrigger : MonoBehaviour {

    public Ballista ballista;

    void OnCollisionEnter2D(Collision2D collider)
    {
        if((collider.gameObject.tag == "Block" || 
            collider.gameObject.tag == "StartBlock" || 
            collider.gameObject.tag =="Bolt" || 
            collider.gameObject.tag == "CatapultBall") && GameManager.instance.inPlay)
        {
            Debug.Log("Arrow Fired");
            ballista.FireArrow();
        }
    }
}

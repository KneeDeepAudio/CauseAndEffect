using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public Portal linkedPortal;
    public bool is_exiting;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!is_exiting)
        {
            linkedPortal.is_exiting = true;
            col.transform.position = linkedPortal.transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        is_exiting = false;
        if(col.tag == "Bolt")
        {
            BallistaArrow bolt = col.GetComponent<BallistaArrow>();
            bolt.NextDirection();
        }
    }
}
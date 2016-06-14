using UnityEngine;
using System.Collections;

public class BallistaArrow : MonoBehaviour {

    public bool active = false;
    public short numReflect = 0;
    public float xUpdate;
    public float yUpdate;

	// Update is called once per frame
	void Update ()
    {
        if (!GameManager.instance.inPlay)
            return;

        if (active)
        {
            this.transform.position = new Vector3(transform.position.x + (xUpdate * Time.deltaTime), transform.position.y + (yUpdate * Time.deltaTime));
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        if(numReflect >= 4)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            xUpdate = 0.0f;
            yUpdate = 0.0f;
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {

        if (!GameManager.instance.inPlay)
            return;

        Vector3 pos = this.gameObject.transform.position;
        Vector3 otherPos = other.gameObject.transform.position;

       // Vector3 horizFwd;
       // Vector3 vertiFwd;

        if(other.gameObject.tag == "Block" || other.gameObject.tag == "EndBlock" || other.gameObject.tag == "Object")
        {
            numReflect = 4;
        }
        if(other.gameObject.tag == "Platform")
        {
            //if(pos.x < otherPos.y)
            //{
            //    ++numReflect;
            //    yUpdate = -yUpdate;
            //    if (yUpdate > 0.0f)
            //        this.gameObject.GetComponent<SpriteRenderer>().flipY = false;
            //    else
            //        this.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            //}
            //if(pos.y < otherPos.y)
            //{
            //    ++numReflect;
            //    yUpdate = -yUpdate;
            //    if (yUpdate > 0.0f)
            //        this.gameObject.GetComponent<SpriteRenderer>().flipY = false;
            //    else
            //        this.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            //}

            //if (xUpdate > 0) // Arrow is traveling right
            //    horizFwd = this.gameObject.transform.TransformDirection(Vector3.right);
            //else // Arrow is traveling left
            //    horizFwd = this.gameObject.transform.TransformDirection(Vector3.left);

            //if (yUpdate > 0) // Arrow is traveling up
            //    vertiFwd = this.gameObject.transform.TransformDirection(Vector3.up);
            //else // Arrow is traveling down
            //    vertiFwd = this.gameObject.transform.TransformDirection(Vector3.down);

            //if (Physics.Raycast(this.gameObject.transform.position, horizFwd))
            //    xUpdate = -xUpdate;
        }


        //if (Physics.Raycast(this.gameObject.transform.position, vertiFwd))
        //    yUpdate = -yUpdate;

        if (other.gameObject.tag == "LRWall")
        {
            ++numReflect;
            xUpdate = -xUpdate;
            if (xUpdate > 0.0f)
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            else
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (other.gameObject.tag == "TBWall")
        {
            ++numReflect;
            yUpdate = -yUpdate;
            if (yUpdate > 0.0f)
                this.gameObject.GetComponent<SpriteRenderer>().flipY = false;
            else
                this.gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
    }
}

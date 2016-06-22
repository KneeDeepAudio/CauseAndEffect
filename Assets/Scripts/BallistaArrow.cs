using UnityEngine;
using System.Collections;

public class BallistaArrow : MonoBehaviour
{

    public bool active = false;
    public short numReflect = 0;
    public float xUpdate;
    public float yUpdate;

    private SpriteRenderer sprite;
    private Rigidbody2D body;
    private BoxCollider2D col;

    private GameObject parentBalista;
    private Vector2 travelForce;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        parentBalista = transform.parent.gameObject;
    }

    void Start()
    {
        body.isKinematic = true;
        col.enabled = false;
        travelForce = new Vector2(xUpdate, yUpdate);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            //this.transform.position = new Vector3(transform.position.x + (xUpdate * Time.deltaTime), transform.position.y + (yUpdate * Time.deltaTime));
            //body.AddForce(travelForce, ForceMode2D.Force);
            sprite.enabled = true;
            body.isKinematic = false;
            col.enabled = true;

            if (numReflect >= 4)
            {
                sprite.enabled = false;
                body.isKinematic = true;
                col.enabled = false;
                xUpdate = 0.0f;
                yUpdate = 0.0f;
            }
        }
    }

    public void Shoot(float direction)
    {
        body.velocity = new Vector2(xUpdate*direction, yUpdate);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Don't check for collisions on self
        if (other.gameObject == parentBalista)
        {
            return;
        }

        Vector3 pos = this.gameObject.transform.position;
        Vector3 otherPos = other.gameObject.transform.position;

        // Vector3 horizFwd;
        // Vector3 vertiFwd;

        if (other.gameObject.tag == "Block" || other.gameObject.tag == "EndBlock" || other.gameObject.tag == "Object")
        {
            numReflect = 4;
        }
        else
        {
            numReflect++;
        }


        /*

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
                sprite.flipX = false;
            else
                sprite.flipX = true;
        }
        if (other.gameObject.tag == "TBWall")
        {
            ++numReflect;
            yUpdate = -yUpdate;
            if (yUpdate > 0.0f)
                sprite.flipY = false;
            else
                sprite.flipY = true;
        }

        */
    }

    void OnEnable()
    {
        //GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    void OnDisable()
    {
        //GameEventManager.GameReset -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
    }

    void GameReset()
    {
        body.isKinematic = true;
    }
}

using UnityEngine;
using System.Collections;

public class Ballista : MonoBehaviour {

    public BallistaArrow arrow;
    public Sprite frame0;
    public Sprite frame1;
    public bool hasFired = false;

    private Vector3 arrowPos;

    void OnEnable()
    {
        GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset  += GameReset;
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    void OnDisable()
    {
        GameEventManager.GameReset -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
    }

    public void FireArrow()
    {
        if(!hasFired)
        {
            hasFired = true;
            arrow.active = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = frame1;
            arrow.Shoot();
        }        
    }

    public void GameLaunch()
    {
        if (!arrow)
            return;
        arrowPos = arrow.transform.position;
        hasFired = false;
        arrow.active = false;
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }
   /* public void OnMouseDown ()
        {
        if (changed)
            return;
        direction *= -1;
        Debug.Log("Flip: " + gameObject.transform.position);
        //gameObject.transform.Rotate(0f,180,0f);
        transform.rotation = new Quaternion(0f,180f,0,0);
        ballPosition = catapultBall.transform.position;
        changed = true;
        }
        */
    public void GameReset()
    {
        hasFired = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = frame0;
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

        arrow.active = false;
        arrow.transform.position = arrowPos;
        arrow.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        arrow.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        arrow.gameObject.GetComponent<SpriteRenderer>().flipY = false;
        arrow.xUpdate = 10.0f;
        arrow.yUpdate = 7.5f;
        arrow.numReflect = 0;
    }
}

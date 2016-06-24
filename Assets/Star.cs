using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

    bool wasHit = false;

    void OnEnable()
    {
        GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
    }

    void OnDisable()
    {
        GameEventManager.GameReset -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
    }

    // Update is called once per frame
    void Update ()
    {    
        if(wasHit)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled  = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.gameObject.GetComponent<BoxCollider2D>().enabled  = true;
        }
    }

    public void GameLaunch()
    {
        wasHit = false; 
    }

    public void GameReset()
    {
        wasHit = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Block" || other.gameObject.tag == "Arrow" || 
           other.gameObject.tag == "CatapultBall" || other.gameObject.tag == "StartBlock")
        {
            wasHit = true;
        }
    }
}

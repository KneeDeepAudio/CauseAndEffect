using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

    [Range(1,2)]
    public int starIndex;

    void OnEnable()
    {
        //GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
    }

    void OnDisable()
    {
        //GameEventManager.GameReset -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
    }

    public void GameReset()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Block" || other.gameObject.tag == "Bolt" || 
           other.gameObject.tag == "CatapultBall" || other.gameObject.tag == "StartBlock")
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

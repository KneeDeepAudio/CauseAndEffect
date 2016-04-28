using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour {

    public float bounciness;
    public PhysicsMaterial2D bounceMat;
    public PhysicsMaterial2D flatMat;

    private BoxCollider2D col;
    private Rigidbody2D rBody;
    private AudioSource trampContact;


    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rBody = GetComponent<Rigidbody2D>();
        trampContact = GetComponent<AudioSource>();
    }

    void GameLaunch()
    {
        col.sharedMaterial = bounceMat;
        rBody.constraints = RigidbodyConstraints2D.FreezeAll ;
    }

    void GameReset()
    {
        col.sharedMaterial = flatMat;
        rBody.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }

    void OnEnable()
    {
        GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
    }

    void OnDisable()
    {
        GameEventManager.GameLaunch -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        trampContact.Play();

        /*
        if (collider.gameObject.tag == "Block" && collided == false)
        {
            
            collided = true;
        }
        */
    }
}

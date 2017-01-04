using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour {

    public CatapultBall catapultBall;
    //public SpriteRenderer catapultImage;
    //public Sprite[] catapultAnimation = new Sprite[3];
    public Vector2 ballForce;
    public bool hasFired = false;
    int direction = 1; //1 = facing right, -1 = facing left
    bool changed = false;

    //public Transform ballLocation;
    //private Vector3 ballPosition;
    private AudioSource source;
    private float clickTime;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        //catapultImage = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {

        //if (catapultImage.sprite != catapultAnimation[0])
        //    catapultImage.sprite = catapultAnimation[0];

        GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
    }

    void OnMouseDown()
    {
        clickTime = Time.time;
    }

    void OnMouseUp()
    {
        if (Time.time - clickTime < 0.35 && !GameManager.instance.inPlay)
        {
            Flip();
        }
    }

    public void LaunchBall()
    {
        if (hasFired == false)
        {
            //StartCoroutine("CatapultAnimation");
            source.Play();
            hasFired = true;
            catapultBall.Travel(ballForce, direction);
        }
    }

    IEnumerator CatapultAnimation ()
    {
        for (int i = 0 ; i < 3 ; ++i)
        {
            //catapultImage.sprite = catapultAnimation[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Flip ()
    {
        Debug.Log("FLIPPED THE BITCH");
        if (changed)
        {
            direction = 1;
            Debug.Log("Flip: " + gameObject.transform.position);
            //gameObject.transform.Rotate(0f,180,0f);
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            //ballPosition = catapultBall.transform.position;
            changed = false;
        }
        else if (!changed)
        {
            direction = -1;
            Debug.Log("Flip: " + gameObject.transform.position);
            //gameObject.transform.Rotate(0f,180,0f);
            transform.rotation = new Quaternion(0f, 180f, 0, 0);
            //ballPosition = catapultBall.transform.position;
            changed = true;
        }
    }

    public void GameLaunch()
    {
        hasFired = false;
        changed = false;   
    }

    public void GameReset()
    {
        //if (catapultImage.sprite != catapultAnimation[0])
        //    catapultImage.sprite = catapultAnimation[0];
    }

    void OnDisable()
    {
        GameEventManager.GameLaunch -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
    }



}

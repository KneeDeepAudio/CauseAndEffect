using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour {

    public GameObject catapultBall;
    SpriteRenderer catapultImage;
    public Sprite[] catapultAnimation;
    public Vector2 ballForce;
    public bool hasFired = false;
    int direction = 1; //1 = facing right, -1 = facing left
    bool changed = false;
    private Vector3 ballPosition;
    private Rigidbody2D ballBody;
    private AudioSource catapultLaunch;

    void Awake()
    {
        catapultImage = transform.Find("Catapult").GetComponent<SpriteRenderer>();
        catapultBall = transform.Find("Ball").gameObject;
        ballBody = catapultBall.GetComponent<Rigidbody2D>();
        catapultLaunch = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        catapultImage.sprite = catapultAnimation[0];
        ballPosition = catapultBall.transform.position;
        GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
    }

    public void LaunchBall()
    {
        if (hasFired == false)
        {
            StartCoroutine("CatapultAnimation");
            catapultLaunch.Play();
            ballBody.AddForce(new Vector2(ballForce.x*direction,ballForce.y));
            hasFired = true;
            catapultBall.GetComponent<CatapultBall>().Travel();
        }
    }

    IEnumerator CatapultAnimation ()
        {
        for (int i = 0 ; i < 3 ; ++i)
            {
            catapultImage.sprite = catapultAnimation[i];
            yield return new WaitForSeconds(0.1f);
            }
        }

    public void OnMouseDown ()
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

    public void GameLaunch()
    {
        hasFired = false;
        changed = false;
        
    }

    public void GameReset()
    {
        catapultImage.sprite = catapultAnimation[0];
        ballBody.isKinematic = true;
        catapultBall.transform.position = ballPosition;
        ballBody.isKinematic = false;
    }

    void OnDisable()
    {
        GameEventManager.GameReset -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
    }

}

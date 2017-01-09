using UnityEngine;
using System.Collections;

public class Ballista : MonoBehaviour {

    public BallistaArrow arrow;
    public Sprite frame0;
    public Sprite frame1;
    public SpriteRenderer ballistaSprite;
    public bool hasFired = false;

    private Vector3 arrowPos;
    private SpriteRenderer arrowSprite;
    private bool changed;
    private float direction = 1;

    private float clickTime;

    void Awake()
    {
        arrowSprite = arrow.GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset  += GameReset;
    }

    void OnDisable()
    {
        GameEventManager.GameReset -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
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

    public void FireArrow()
    {
        if(!hasFired)
        {
            hasFired = true;
            arrow.Shoot();
            ballistaSprite.sprite = frame1;
        }        
    }

    public void Flip()
    {
        Debug.Log("FLIPPED THE BITCH");
        if (changed)
        {
            direction *= -1;
            Debug.Log("Flip: " + gameObject.transform.position);
            //gameObject.transform.Rotate(0f,180,0f);
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            arrowPos = arrow.transform.position;
            changed = false;
        }
        else if (!changed)
        {
            direction *= -1;
            Debug.Log("Flip: " + gameObject.transform.position);
            //gameObject.transform.Rotate(0f,180,0f);
            transform.rotation = new Quaternion(0f, 180f, 0, 0);
            arrowPos = arrow.transform.position;
            changed = true;
        }
    }

    public void GameLaunch()
    {
        if (!arrow)
            return;
        arrowPos = arrow.transform.position;
        hasFired = false;
    }

    public void GameReset()
    {
        hasFired = false;
        ballistaSprite.sprite = frame0;

        arrow.transform.position = arrowPos;
        arrowSprite.enabled = false;
        arrowSprite.flipX = false;
        arrowSprite.flipY = false;
    }
}

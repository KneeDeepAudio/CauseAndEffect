using UnityEngine;
using System.Collections;

public class BallistaArrow : MonoBehaviour
{
    public float speed;
    public int bounces = 4;
    public GameObject rayOrigin;

    private Rigidbody2D body;
    private Ray2D b;
//    private Vector2 nextDirection;
    private SpriteRenderer sprite;
    private PolygonCollider2D col;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private int bouncesLeft;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        Debug.DrawRay(rayOrigin.transform.position, rayOrigin.transform.right);
    }

    /*
    void Update()
    {
        Ray2D a = new Ray2D(rayOrigin.transform.position, rayOrigin.transform.right);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.transform.position, rayOrigin.transform.right, 10f, 1);

        if (Deflect(a, out b, hit))
        {
            Debug.DrawLine(a.origin, hit.point);
            Debug.DrawLine(b.origin, b.origin + 3 * b.direction);
        }
    }
    */

    bool Deflect(Ray2D ray, out Ray2D deflected, RaycastHit2D hit)
    {

        if (Physics2D.Raycast(rayOrigin.transform.position, rayOrigin.transform.right))
        {
            Vector2 normal = hit.normal;
            Vector2 deflect = Vector2.Reflect(ray.direction, normal);

            deflected = new Ray2D(hit.point, deflect);
            return true;
        }

        deflected = new Ray2D(Vector3.zero, Vector3.zero);
        return false;
    }

    public void NextDirection()
    {
        Ray2D a = new Ray2D(rayOrigin.transform.position, rayOrigin.transform.right);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.transform.position, rayOrigin.transform.right, 10f, 1);

        if (Deflect(a, out b, hit))
        {
            //nextDirection = new Vector2(b.direction.x, b.direction.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        bouncesLeft--;

        if (bouncesLeft == 0)
        {
            TurnOff();
            return;
        }

        Vector3 endPoint = b.origin + 3 * b.direction;
        Vector3 dir = endPoint - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        body.velocity = new Vector2(transform.right.x, transform.right.y) * speed;
        NextDirection();
    }

    public void TurnOff()
    {
        sprite.enabled = false;
        body.velocity = Vector2.zero;
        body.angularVelocity = 0f;
        col.enabled = false;
        transform.position = startPosition;
        transform.rotation = startRotation;        
    }

    public void Shoot()
    {
        bouncesLeft = bounces;
        sprite.enabled = true;
        col.enabled = true;
        body.velocity = new Vector2(transform.right.x, transform.right.y) * speed;
        NextDirection();
    }

    public void Flip()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void GameLaunch()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void GameReset()
    {
        TurnOff();
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
}

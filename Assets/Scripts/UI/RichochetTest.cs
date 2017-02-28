using UnityEngine;
using System.Collections;

public class RichochetTest : MonoBehaviour {

    public float speed;
    private int bounces = 4;
    public GameObject rayOrigin;

    private Rigidbody2D body;
    private Ray2D b;

    private SpriteRenderer sprite;
    private PolygonCollider2D col;

    void Awake () {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<PolygonCollider2D>();
    }

    
    void Start()
    {
        TurnOn();
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

    void NextDirection()
    {
        Ray2D a = new Ray2D(rayOrigin.transform.position, rayOrigin.transform.right);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.transform.position, rayOrigin.transform.right, 10f, 1);
        
        if (Deflect(a, out b, hit))
        {
//            nextDirection = new Vector2(b.direction.x, b.direction.y);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (bounces == 0)
        {
            TurnOff();
        }

        Vector3 endPoint = b.origin + 3 * b.direction;
        Vector3 dir = endPoint - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        body.velocity = new Vector2(transform.right.x, transform.right.y) * speed;
        NextDirection();
    }

    void TurnOff()
    {
        sprite.enabled = false;
        body.isKinematic = true;
        col.enabled = false;
    }

    void TurnOn()
    {
        sprite.enabled = true;
        body.isKinematic = false;
        col.enabled = true;
        body.velocity = new Vector2(transform.right.x, transform.right.y) * speed;
        NextDirection();
    }
    
}

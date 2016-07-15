using UnityEngine;
using System.Collections;

public class RichochetTest : MonoBehaviour {

    public float speed;
    public GameObject rayOrigin;

    private Rigidbody2D body;
    private LayerMask bounce;
    private Ray2D b;
    private Vector2 nextDirection;

    void Awake () {
        body = GetComponent<Rigidbody2D>();
    }

    
    void Start()
    {
        body.velocity = new Vector2(transform.right.x, transform.right.y) * speed;
        bounce = LayerMask.NameToLayer("Default");
        NextDirection();
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
            Debug.DrawLine(a.origin, hit.point);
            Debug.DrawLine(b.origin, b.origin + 3 * b.direction);
            Debug.Log(b.origin + "and " + b.direction);
            nextDirection = new Vector2(b.direction.x, b.direction.y);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        //Physics2D.IgnoreCollision(collider.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Vector3 endPoint = b.origin + 3 * b.direction;
        Vector3 dir = endPoint - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        body.velocity = nextDirection * speed;
        NextDirection();
    }
    
}

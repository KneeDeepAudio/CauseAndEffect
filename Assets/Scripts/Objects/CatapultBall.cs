using UnityEngine;
using System.Collections;

public class CatapultBall : MonoBehaviour
{
    public AudioClip launchSound;
    public AudioClip contactSound;

    private AudioSource ballSource;
    private Rigidbody2D body;
    private Vector3 ballPosition = Vector3.zero;

    void Awake()
    {
        ballSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
    }

    public void Travel(Vector2 ballForce, int direction)
    {
        body.AddForce(new Vector2(ballForce.x * direction, ballForce.y));
        ballSource.Stop();
        ballSource.clip = launchSound;
        ballSource.Play();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        ballSource.Stop();
        ballSource.clip = contactSound;
        ballSource.Play();
    }

    void GameLaunch()
    {
        ballPosition = transform.localPosition;
    }

    void GameReset()
    {
        body.velocity = Vector2.zero;
        body.angularVelocity = 0f;
        transform.localPosition = ballPosition;
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

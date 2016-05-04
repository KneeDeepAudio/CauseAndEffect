using UnityEngine;
using System.Collections;

public class CatapultBall : MonoBehaviour
{
    public AudioClip launchSound;
    public AudioClip contactSound;

    private AudioSource ballSource;

    void Awake()
    {
        ballSource = GetComponent<AudioSource>();
    }

    public void Travel()
    {
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

}

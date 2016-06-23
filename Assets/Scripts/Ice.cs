using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = GetComponent<Transform>().position;
        initialRotation = GetComponent<Transform>().rotation;
    }

    void GameReset()
    {
        transform.rotation = initialRotation;
        transform.position = initialPosition;
    }

    void OnEnable()
    {
        GameEventManager.GameReset += GameReset;
    }

    void OnDisable()
    {
        GameEventManager.GameReset -= GameReset;
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "Bolt")
        {
            GameObject dumpPoint = GameObject.FindGameObjectWithTag("Bucket");
            if (dumpPoint != null) this.transform.position = dumpPoint.transform.position;
        }
    }
}

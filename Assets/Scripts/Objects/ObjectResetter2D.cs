using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectResetter2D : MonoBehaviour {

    public bool destroyOnReset;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private List<Transform> originalStructure;

    private Rigidbody2D Rigidbody;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    private void Start()
    {
        originalStructure = new List<Transform>(GetComponentsInChildren<Transform>());
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void ResetObject()
    {
        // remove any gameobjects added (fire, skid trails, etc)
        //foreach (Transform t in GetComponentsInChildren<Transform>())
        //{
        //    if (!originalStructure.Contains(t))
        //    {
        //        t.parent = null;
        //    }
        //}

        if (Rigidbody)
        {
            Rigidbody.velocity = Vector2.zero;
            Rigidbody.angularVelocity = 0f;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    void OnEnable()
    {
        GameEventManager.GameReset += ResetObject;
    }

    void OnDisable()
    {
        GameEventManager.GameReset -= ResetObject;
    }
}

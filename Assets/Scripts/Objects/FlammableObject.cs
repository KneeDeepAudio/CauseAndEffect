using System.Collections;
using UnityEngine;
using UnityStandardAssets.Utility;

public class FlammableObject : MonoBehaviour
{

    [Tooltip("Can this thing catch fire?")]
    public bool flammable;
    [Tooltip("Is it on fire?")]
    public bool onFire;
    [Tooltip("Can fire destroy this thing?")]
    public bool destroyable;
    [Tooltip("How long until fire destroys this thing. Ignore if indistructable")]
    public float secondsToDestroy;

    public ParticleSystem flames;

    private bool is_flammable;
    private bool is_OnFire;

    // Use this for initialization
    void Start()
    {
        is_flammable = flammable;
        is_OnFire = onFire;
    }

    void GameReset()
    {
        //RemoveFire();
        StopAllCoroutines();
        flammable = is_flammable;
        onFire = is_OnFire;
    }

    void OnEnable()
    {
        GameEventManager.GameReset += GameReset;
    }

    void OnDisable()
    {
        GameEventManager.GameReset -= GameReset;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        FlammableObject otherFlammable = other.gameObject.GetComponent<FlammableObject>();

        if(otherFlammable != null)
        {
            if (otherFlammable.onFire && flammable && !onFire)
            {
                onFire = true;
                StartFire(other);
                if (destroyable)
                    StartCoroutine(StartDestroy());
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        FlammableObject otherFlammable = other.gameObject.GetComponent<FlammableObject>();

        if (otherFlammable != null)
        {
            if (otherFlammable.onFire && flammable && !onFire)
            {
                onFire = true;
                StartFire(other);
                if (destroyable)
                    StartCoroutine(StartDestroy());
            }
        }
    }

    void StartFire(Collision2D col)
    {
        ContactPoint2D contact = col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        ParticleSystem fire = Instantiate(flames, transform.position, Quaternion.identity);
        fire.transform.parent = gameObject.transform;

        ParticleSystemDestroyer fireDestroyer = fire.GetComponent<ParticleSystemDestroyer>();
        if (fireDestroyer != null)
            fireDestroyer.maxDuration = secondsToDestroy;
    }

    //void RemoveFire()
    //{
    //    Transform[] children = GetComponentsInChildren<Transform>();
    //    foreach (Transform child in children)
    //        if (child.CompareTag("Fire"))
    //        {
    //            Destroy(child.gameObject);
    //        }
    //}

    IEnumerator StartDestroy()
    {
        GameObject dumpPoint = GameObject.FindGameObjectWithTag("Bucket");
        yield return new WaitForSeconds(secondsToDestroy);
        if (dumpPoint != null)
            transform.position = dumpPoint.transform.position;
        else
            Debug.Log("Error: No Bucket Found");
    }
}

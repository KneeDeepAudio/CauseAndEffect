using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour {

    public bool full = false;

    private SpriteRenderer sprite;
    private Collider2D col2D;

    private GameObject placedObject;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();
    }

    public bool IsFull
    {
        get
        {
            return this.full;
        }
    }

    public void PlaceObject(GameObject thing)
    {
        Vector3 position = transform.position;
        GameObject spawnedObject = Instantiate(thing, position, Quaternion.identity) as GameObject;

        PlaceableObject spawned = spawnedObject.GetComponent<PlaceableObject>();
        if (spawned != null)
            spawned.spawnArea = this;

        placedObject = spawnedObject;
        full = true;
    }

    public void RemoveObject()
    {
        Destroy(placedObject);
        full = false;
    }

    public void Highlight()
    {
        sprite.enabled = true;
        col2D.enabled = true;
    }

    public void RemoveHighlight()
    {
        sprite.enabled = false;
        col2D.enabled = false;
    }

}

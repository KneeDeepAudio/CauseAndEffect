using UnityEngine;
using System.Collections;

public class PlaceableObject : MonoBehaviour
{

    public PlacementArea areaPlaced;

    public ObjectSpawn spawnArea;

    private bool changed;
    private float direction;
    private Catapult catapult;
    private Ballista ballista;

    void Awake()
    {
        catapult = GetComponent<Catapult>();
        ballista = GetComponent<Ballista>();
    }

    public void ObjectFlip()
    {
        if (catapult != null)
        {
            catapult.Flip();
        }
        else if(ballista != null)
        {
            ballista.Flip();
        }
    }
}
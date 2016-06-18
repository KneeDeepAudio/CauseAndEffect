using UnityEngine;
using System.Collections;

public class LavaPlatform : MonoBehaviour {

    public float secondsToDestroy = 3f;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bolt")
        {
            Debug.Log("Lava Hit!");
            Destroy(col.gameObject);
        }
        
        else
        {
            StartCoroutine(DestroyObject(col.gameObject));
        } 
    }

    IEnumerator DestroyObject(GameObject toDestroy)
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(toDestroy);
    }
}

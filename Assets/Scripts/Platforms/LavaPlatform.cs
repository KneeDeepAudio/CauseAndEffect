using UnityEngine;
using System.Collections;

public class LavaPlatform : MonoBehaviour {

    //public float secondsToDestroy = 3f;

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "Bolt")
    //    {
    //        Debug.Log("Lava Hit!");
    //        DumpThing(col.gameObject);
    //    }
        
    //    else
    //    {
    //        StartCoroutine(DumpObject(col.gameObject));
    //    } 
    //}

    //IEnumerator DumpObject(GameObject toDestroy)
    //{
    //    yield return new WaitForSeconds(secondsToDestroy);
    //    DumpThing(toDestroy);
    //}

    //void DumpThing(GameObject thing)
    //{
    //    GameObject dumpPoint = GameObject.FindGameObjectWithTag("Bucket");
    //    if(dumpPoint != null) thing.transform.position = dumpPoint.transform.position;
    //}

    //void GameReset()
    //{
    //    StopAllCoroutines();
    //}

    //void OnEnable()
    //{
    //    GameEventManager.GameReset += GameReset;
    //}

    //void OnDisable()
    //{
    //    GameEventManager.GameReset -= GameReset;
    //}

}
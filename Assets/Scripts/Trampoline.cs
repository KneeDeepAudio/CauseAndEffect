using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour {

    public float bounciness;
    public PhysicsMaterial2D bounceMat;
    public PhysicsMaterial2D flatMat;

    private BoxCollider2D col;



    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    void GameLaunch()
    {
        col.sharedMaterial = bounceMat;
    }

    void GameReset()
    {
        col.sharedMaterial = flatMat;
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

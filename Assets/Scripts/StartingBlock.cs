using UnityEngine;
using System.Collections;

public class StartingBlock : MonoBehaviour {

    public float force = 45;

    public Rigidbody2D rBody;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    public void AddTorque(float torque, ForceMode2D mode = ForceMode2D.Force) { }

}

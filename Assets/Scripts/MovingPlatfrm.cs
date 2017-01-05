using UnityEngine;
using System.Collections;

public class MovingPlatfrm : MonoBehaviour 
{
    [Range(2,4)]
    public int numberOfPlatforms;

    public Transform platform;
	public Transform[] positions;
	public float  platformSpeed;

    [Range(0.0f, 1.0f)]
	public float gizmoSize = 0.15f;
	public bool looping;
    public bool continuous;

    //Vector3 initialPosition;
    //Quaternion initialRotation;

	private Rigidbody2D rb;
	private Vector3 nextPosition;
	private int destination;
	private bool forward = true;
    private bool reachedEnd = false;

	void Start()
	{
        ///Set Initial position and rotation for reseting
        platform.position = positions[0].transform.position;
        //initialPosition = platform.transform.position;
        //initialRotation = platform.transform.rotation;

        rb = platform.GetComponent<Rigidbody2D> ();
       
        destination = 1;
		SetDestination (destination);
	}

	void Update ()
	{

        //if (!GameManager.instance.inPlay)
        //    return;

        if (!continuous && reachedEnd)
        {
            return;
        }

        rb.MovePosition(platform.position + nextPosition * platformSpeed * Time.fixedDeltaTime);        //Moving the platform

        if (Mathf.Abs(Vector3.Distance(platform.position,positions[destination].position)) < 0.5f)      //If nearing destination
        {
            if (forward && (destination + 1 < numberOfPlatforms))                                       //If there is a next poistion && yet to reach the last platform, continue on to that
            {
                destination++;
                SetDestination(destination);
            }
            else                                                                                        //If the platform has reached its last position || has started on it's reverse journey
            {
                reachedEnd = true;
                if (looping)                                                                            //If looping, set the next destination as 0 postion
                {
                    destination = 0;
                    SetDestination(destination);
                }
                else                                                                                    //If not looping, start going in reverse
                {
                    if (destination == 0)
                        forward = true;                                                                 //If reached the starting position in reverse, begin forward route
                    else
                    {
                        destination--;
                        SetDestination(destination);
                        forward = false;                                                                //Since last platform reached, starting reverse journey
                    }
                }
            }
        }
    }

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere (positions[0].position, gizmoSize);

		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere (positions[1].position, gizmoSize);

		Gizmos.color = Color.red;
		Gizmos.DrawSphere (positions[2].position, gizmoSize);

		Gizmos.color = Color.magenta;
		Gizmos.DrawSphere (positions[3].position, gizmoSize);
	}

	void SetDestination(int dest)
	{
	    nextPosition = (positions[dest].position - platform.position).normalized; 
	}

    //void GameReset ()
    //{
    //    platform.transform.rotation = initialRotation;
    //    platform.transform.position = initialPosition;
    //}

    void OnEnable ()
    {
        //GameEventManager.GameLaunch += GameLaunch;
        //GameEventManager.GameReset += GameReset;
    }

    void OnDisable ()
    {
        // GameEventManager.GameLaunch -= GameLaunch;
        //GameEventManager.GameReset -= GameReset;
    }
}
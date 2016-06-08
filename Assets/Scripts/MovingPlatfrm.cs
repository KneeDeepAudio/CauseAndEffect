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
	public bool continuous;

    Vector3 initialPosition;
    Quaternion initialRotation;
	private Rigidbody2D rb;
	private Vector3 direction;
	private int destination;
	private bool forward = true;



	void Start()
	{
        ///Set Initial position and rotation for reseting
        platform.position = positions[0].transform.position;
        initialPosition = platform.transform.position;
        initialRotation = platform.transform.rotation;

        rb = platform.GetComponent<Rigidbody2D> ();
       
        destination = 1;
		SetDestination (destination);
	}

	void FixedUpdate ()
	{

        if (!GameManager.instance.inPlay)
            return;

		rb.MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);       //Moving the platform

        if (Mathf.Abs(Vector3.Distance(platform.position,positions[destination].position)) < 1f)    //If nearing destination
            {
            if (forward && (destination + 1) < numberOfPlatforms)                                   //If there is a next poistion && yet to reach the last platform, continue on to that
                {
                SetDestination(++destination);
                }
            else                                                                                    //If the platform has reached its last position || has started on it's reverse journey
                {
                if (continuous)                                                                     //If looping, set the next destination as 0 postion
                    {
                    destination = 0;
                    SetDestination(destination);
                    }
                else                                                                                //If not looping, start going in reverse
                    {
                    if (destination == 0)
                        forward = true;                                                             //If reached the starting position in reverse, begin forward route
                    else
                        {
                        SetDestination(--destination);
                        forward = false;                                                            //Since last platform reached, starting reverse journey
                        }
                    }
                }
            }
	
	}


	void OnDrawGizmos ()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube (positions[0].position, platform.localScale*gizmoSize);

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube (positions[1].position, platform.localScale*gizmoSize);

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (positions[2].position, platform.localScale*gizmoSize);

		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube (positions[3].position, platform.localScale*gizmoSize);
	}

	void SetDestination(int dest)
	{
	direction = (positions[dest].position - platform.position).normalized; 
	}

    void GameReset ()
        {
        platform.transform.rotation = initialRotation;
        platform.transform.position = initialPosition;
        }

    void OnEnable ()
        {
        //GameEventManager.GameLaunch += GameLaunch;
        Debug.Log("Calling Enabledelegate");
        GameEventManager.GameReset += GameReset;
        }

    void OnDisable ()
        {
        // GameEventManager.GameLaunch -= GameLaunch;
        Debug.Log("Calling disabledelegate");
        GameEventManager.GameReset -= GameReset;
        }

    }
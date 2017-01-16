using UnityEngine;
using System.Collections;

public class MovingPlatfrm : MonoBehaviour 
{
    [Tooltip("Select the amount of positions the platform will move between")]
    [Range(2,4)]
    public int numberOfPlatforms;
	public float  platformSpeed;

    [Tooltip("If true, platform will continue after reaching final position")]
    public bool continuous;
    [Tooltip("if true and, platform will move from final position to first, if continuing")]
    public bool looping;
    [Tooltip("If true, platform will wait until trigger is hit to move")]
    public bool isTriggered;

    public Transform platform;
    public Transform[] positions;
    public Transform trigger;

    [Range(0.0f, 1.0f)]
    public float gizmoSize = 0.15f;

    //Vector3 initialPosition;
    //Quaternion initialRotation;

    private Rigidbody2D rb;
	private Vector3 nextPosition;
	private int destination;
	private bool forward = true;
    private bool reachedEnd = false;
    private bool is_triggered;

    private LineRenderer lines;
    private Vector3[] pos;
    
    void Awake()
    {
        lines = GetComponent<LineRenderer>();
        rb = platform.GetComponent<Rigidbody2D>();
    }

	void Start()
	{
        SetLines();

        ///Set Initial position and rotation for reseting
        platform.position = positions[0].transform.position;
        //initialPosition = platform.transform.position;
        //initialRotation = platform.transform.rotation;
       
        destination = 1;
		SetDestination (destination);
        HideTrigger();
        is_triggered = isTriggered;
    }

    void Update ()
	{

        if (isTriggered)
            return;

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

    void SetLines()
    {
        pos = new Vector3[positions.Length + 1]; 
        for(int i = 0; i < positions.Length; i++)
        {
            pos[i] = positions[i].localPosition;
        }
        pos[positions.Length] = pos[0];

        if (numberOfPlatforms < 4)
            lines.numPositions = numberOfPlatforms;
        else
            lines.numPositions = numberOfPlatforms + 1;

        lines.SetPositions(pos);
        //lines.SetPosition(positions.Length, positions[0].localPosition);
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

    void HideTrigger()
    {
        if (!isTriggered)
            trigger.gameObject.SetActive(false);
    }

    void GameReset()
    {
        isTriggered = is_triggered;
        platform.position = positions[0].transform.position;
        destination = 1;
        SetDestination(destination);
    }

    void OnEnable ()
    {
        //GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
    }

    void OnDisable ()
    {
        // GameEventManager.GameLaunch -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
    }
}
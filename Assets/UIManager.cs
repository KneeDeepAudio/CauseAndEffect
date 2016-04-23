using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Button launchButton;
    public float force = 45;
    public bool startRight;

    private float directionModifier;
    private GameObject startBlock;

    void Awake()
    {
        startBlock = GameObject.FindGameObjectWithTag("StartBlock");
        if (startRight == true)
        {
            directionModifier = -1;
        }
        else
        {
            directionModifier = 1;
        }
    }

    void Start()
    {
        StartingBlock startingBlock = startBlock.GetComponent<StartingBlock>();
        launchButton.onClick.AddListener(delegate { startingBlock.rBody.AddTorque(force * directionModifier); } );
    }

}

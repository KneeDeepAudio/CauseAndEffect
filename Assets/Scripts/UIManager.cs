using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Button launchButton;
    public float force = 45;
    public bool startRight;

    public GameObject block;
    public GameObject trampoline;
    public GameObject Catapult;
    public GameObject Balista;

    private float directionModifier;
    private GameObject startBlock;

    void Awake()
    {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
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
        //launchButton.onClick.AddListener(delegate { startingBlock.rBody.AddTorque(force * directionModifier); } );
    }

    public void Restart()
    {
        GameManager.instance.Restart();
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

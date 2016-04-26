using UnityEngine;

public class EndButton : MonoBehaviour {

    private GameObject winText;
    private GameObject continueButton;

    void Awake()
    {
        winText = GameObject.FindGameObjectWithTag("Win Text");
        continueButton = GameObject.FindGameObjectWithTag("ContinueButton");
    }

    void Start()
    {
        winText.SetActive(false);
        continueButton.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Block")
        {
            winText.SetActive(true);
            continueButton.SetActive(true);
        }
    }
}

using UnityEngine;

public class EndButton : MonoBehaviour {

    private GameObject winText;
    private GameObject continueButton;
    private AudioSource hitSound;

    void Awake()
    {
        winText = GameObject.FindGameObjectWithTag("Win Text");
        continueButton = GameObject.FindGameObjectWithTag("ContinueButton");
        hitSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        winText.SetActive(false);
        continueButton.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Block" || collider.gameObject.tag == "CatapultBall")
        {
            hitSound.Play();
            winText.SetActive(true);
            continueButton.SetActive(true);
        }
    }
}

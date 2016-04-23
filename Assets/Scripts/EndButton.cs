using UnityEngine;

public class EndButton : MonoBehaviour {

    private GameObject winText;

    void Awake()
    {
        winText = GameObject.FindGameObjectWithTag("Win Text");
    }

    void Start()
    {
        winText.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Block")
        {
            winText.SetActive(true);
        }
    }
}

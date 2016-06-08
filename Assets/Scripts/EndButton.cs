using UnityEngine;

public class EndButton : MonoBehaviour {


    private AudioSource hitSound;
    private UIManager guiManager;

    void Awake()
    {
        hitSound = GetComponent<AudioSource>();
        guiManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Block" || collider.gameObject.tag == "CatapultBall")
        {
            hitSound.Play();
            guiManager.LevelComplete();
        }
    }
}

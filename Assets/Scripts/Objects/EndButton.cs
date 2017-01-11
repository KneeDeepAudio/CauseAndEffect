using UnityEngine;

public class EndButton : MonoBehaviour {

    public int timesHit = 1;

    private int amountHit;

    private AudioSource hitSound;
    //private UIManager guiManager;

    void Awake()
    {
        hitSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Block" || collider.gameObject.tag == "CatapultBall" || collider.gameObject.tag == "Bolt")
        {
            amountHit++;
            if (amountHit == timesHit)
            {
                hitSound.Play();
                GameEventManager.TriggerLevelComplete();
            }
        }
    }
}
using UnityEngine;
using System.Collections;

public class GameSoundScript : MonoBehaviour {

    public static GameSoundScript instance;
    public AudioClip inGameTrack,titleTrack,playLaunch;
    public AudioClip[] uiButton;
    int uiButtonSelect = 0;
    AudioSource gameAudioSource,sfxAudioSource;


	// Use this for initialization
	void Start () {

        if (instance == null)
            instance = this;

        DontDestroyOnLoad(gameObject);
        gameAudioSource = GetComponents<AudioSource>()[0];
        sfxAudioSource = GetComponents<AudioSource>()[1];
        playTitleTrack();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playTitleTrack ()
        {
        gameAudioSource.clip = titleTrack;
        gameAudioSource.Play();
        }

    public void playInGameTrack ()
        {
        gameAudioSource.clip = inGameTrack;
        gameAudioSource.Play();
        }

    public void playLaunchGameSound ()
        {
        sfxAudioSource.clip = playLaunch;
        sfxAudioSource.Play();
        }

    public void playUIButton ()
        {
        sfxAudioSource.clip = uiButton[uiButtonSelect+1 > 1 ? 0 : uiButtonSelect+1];
        sfxAudioSource.Play();
        }

    }

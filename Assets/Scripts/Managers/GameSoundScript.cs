using UnityEngine;
using UnityEngine.SceneManagement;
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

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex > 2)
            Destroy(gameObject);//playInGameTrack();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

}

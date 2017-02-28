using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public AudioClip pauseClip, launchClip;
    public GameObject pausePanel;

    private AudioSource uiAudioSource;


    void Awake()
    {
        uiAudioSource = GetComponent<AudioSource>();
    }

    public void Button_Reset()
    {
        GameManager.instance.GameReset();
    }

    public void Button_Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Button_ClearAll()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Button_Launch()
    {
        GameManager.instance.GameLaunch();
    }

    public void Button_LevelSelect()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelect");
    }

    public void Button_MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }

    void GameReset()
    {
        uiAudioSource.clip = pauseClip;
        uiAudioSource.Play();
    }

    void GameLaunch()
    {
        uiAudioSource.clip = launchClip;
        uiAudioSource.Play();
    }

    public void ChangeCurrentObject(GameObject newObject)
    {
        InputManager.instance.currentObject = newObject;
        ShowHighlights(newObject.tag);
    }


    void OnEnable()
    {
        GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
    }

    void OnDisable()
    {
        GameEventManager.GameLaunch -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
    }

    public void Pause ()
    {
        if (Time.timeScale == 1)
            {
            Time.timeScale = 0;
            //pausePanel.SetActive(true);
            }
        else if (Time.timeScale == 0)
            {
            Time.timeScale = 1;
            //pausePanel.SetActive(false);
            }
    }

    public void ShowHighlights(string tag)
    {
        if (tag == "Object")
        {
            InputManager.instance.ShowHighlights("Object");
        }
        else if(tag == "CatapultBall")
        {
            InputManager.instance.ShowHighlights("Sphere");
        }
        else if (tag == "Block")
        {
            InputManager.instance.ShowHighlights("Block");
        }
    }
}

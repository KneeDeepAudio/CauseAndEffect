using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Button launchButton;

    private GameObject winText;
    private GameObject continueButton;
    public GameObject pausePanel;

    void Awake()
    {
        winText = GameObject.FindGameObjectWithTag("Win Text");
        continueButton = GameObject.FindGameObjectWithTag("ContinueButton");

        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    void Start()
    {
        winText.SetActive(false);
        continueButton.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //GameManager.instance.Restart();
    }

    public void LevelComplete()
    {
        winText.SetActive(true);
        continueButton.SetActive(true);
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void GameReset()
    {
        launchButton.onClick.RemoveAllListeners();
        launchButton.onClick.AddListener(delegate { GameManager.instance.Launch(); });
        launchButton.GetComponentInChildren<Text>().text = "Play";
    }

    void GameLaunch()
    {
        launchButton.onClick.RemoveAllListeners();
        launchButton.onClick.AddListener(delegate { GameManager.instance.Restart(); } );
        launchButton.GetComponentInChildren<Text>().text = "Reset";
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
            pausePanel.SetActive(true);
            }
        else if (Time.timeScale == 0)
            {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            }
        }

    public void LevelSelectUI ()
        {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelect");
        }

    public void MainMenuUI ()
        {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
        }

    }

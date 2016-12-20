using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Button launchButton;
    public GameObject winText;
    public GameObject continueButton;
    public AudioClip pauseClip, launchClip;
    public GameObject pausePanel;

    private GameManager manager;
    private InputManager inputManger;
    private AudioSource uiAudioSource;
    private PlacementArea[] placementAreas;

    void Awake()
    {
        //winText = GameObject.FindGameObjectWithTag("Win Text");
        //continueButton = GameObject.FindGameObjectWithTag("ContinueButton");
        uiAudioSource = GetComponent<AudioSource>();
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        inputManger = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        placementAreas = GameObject.FindObjectsOfType<PlacementArea>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    void Start()
    {
        //winText.SetActive(false);
        //continueButton.SetActive(false);
        //launchButton.onClick.RemoveAllListeners();
        //launchButton.onClick.AddListener(delegate { Launch(); });
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
        uiAudioSource.clip = pauseClip;
        winText.SetActive(false);
        continueButton.SetActive(false);
        uiAudioSource.Play();
        //launchButton.onClick.RemoveAllListeners();
        //launchButton.onClick.AddListener(delegate { GameManager.instance.Launch(); });
        launchButton.GetComponentInChildren<Text>().text = "Play";
    }

    void GameLaunch()
    {
        uiAudioSource.clip = launchClip;
        uiAudioSource.Play();
        //launchButton.onClick.RemoveAllListeners();
        //launchButton.onClick.AddListener(delegate { GameManager.instance.Restart(); } );
        launchButton.GetComponentInChildren<Text>().text = "Reset";
    }

    public void ChangeCurrentObject(GameObject newObject)
    {
        inputManger.currentObject = newObject;
        ShowHighlights();

        //canvasSource.clip = chooseObject[chooseObjecti + 1 < chooseObject.Length ? ++chooseObjecti : chooseObjecti = 0];
        //canvasSource.Play();   
    }

    public void ClearAll()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Launch()
    {
        manager.Launch();
    }

    public void Reset()
    {
        manager.Restart();
    }

    void OnEnable()
    {
        GameEventManager.GameLaunch += GameLaunch;
        GameEventManager.GameReset += GameReset;
        GameEventManager.LevelComplete += LevelComplete;
    }

    void OnDisable()
    {
        GameEventManager.GameLaunch -= GameLaunch;
        GameEventManager.GameReset -= GameReset;
        GameEventManager.LevelComplete -= LevelComplete;
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

    public void ShowHighlights()
    {
        if (inputManger.currentObject.tag == "Object")
        {
            foreach (PlacementArea area in placementAreas)
            {
                area.HighlightObjectPlacement();
            }
        }
        else if (inputManger.currentObject.tag == "Block")
        {
            foreach (PlacementArea area in placementAreas)
            {
                area.HighlightBlockPlacement();
            }
        }
    }
}

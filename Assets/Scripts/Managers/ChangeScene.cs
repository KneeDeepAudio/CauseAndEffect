using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour
{

    public string transitionScene;
    public float transitionTimer;
    public bool isAutomated = false; 

    void Update()
    {
        if (transitionTimer > 0.0f && isAutomated)
        {
            transitionTimer -= Time.deltaTime;
        }

        if (transitionTimer <= 0.0f)
        {
            SceneManager.LoadScene(transitionScene);
        }
    }

    public void ChangeScene_Button(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour {

    public void LevelSelect(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }


}

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour {

    public void LevelSelect(int levelIndex)
    {
        GameSoundScript.instance.playUIButton();
        SceneManager.LoadScene(levelIndex);
    }


}

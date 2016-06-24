using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour {

    public void LevelSelect(string sceneToLoad)
    {
        if(GameSoundScript.instance!=null)
        GameSoundScript.instance.playUIButton();
        SceneManager.LoadScene(sceneToLoad);
    }
}

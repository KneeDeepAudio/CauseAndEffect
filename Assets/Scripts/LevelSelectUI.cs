using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour {

    public void LevelSelect(int levelIndex)
    {

        if(GameSoundScript.instance!=null)
        GameSoundScript.instance.playUIButton();

        SceneManager.LoadScene(levelIndex);
    }


}

using UnityEngine;
using System.Collections;

/* USES:
 * ==============
 * Menu.cs
 * ==============
 * 
 * USAGE:
 * ======================================
 * Used to call method to open menus through 
 * UI buttons. UI button calls the method and 
 * the script sets the animator trigger bools
 * ======================================
 */

public class MenuManager : MonoBehaviour
{

    public Menu startMenu;
    public Menu completionMenu;
    private Menu currentMenu;
    

    void Start()
    {
        currentMenu = startMenu;
        // Open Starting Menu
        ShowMenu(currentMenu);
    }

    // used to open new menus. Called by buttons
    public void ShowMenu(Menu menu)
    {
        // Close any current menus by setting IsOpen to false
        if (currentMenu != null) currentMenu.IsOpen = false;

        // Set new menu and set IsOpen to true
        currentMenu = menu;
        currentMenu.IsOpen = true;
    }

    public void LevelComplete()
    {
        ShowMenu(completionMenu);
    }

    void OnEnable()
    {
        GameEventManager.LevelComplete += LevelComplete;
    }

    void OnDisable()
    {
        GameEventManager.LevelComplete -= LevelComplete;
    }

}

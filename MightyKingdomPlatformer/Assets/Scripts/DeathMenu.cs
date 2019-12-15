using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    // This is the title of the main menu.
    public string mainMenuTitle;

    // This is a reference to the game manager.
    public GameManagement gameManager;


  
    // When we hit the main menu button, load the main menu.
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuTitle);
   
    }

    // When we hit the Restart button, restart the game.
    public void Restart()
    {
        gameManager.Reset();
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManagement : MonoBehaviour
{
    public string playGameName;

    // When we hit play game, load the game.
    public void PlayGame()
    {
        SceneManager.LoadScene(playGameName);
    }
    public void QuitGame()
    {
        // Once we click this, the game will stop running.
        Application.Quit();
    }
}

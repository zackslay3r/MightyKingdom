using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public string mainMenuName;
    // We need to hace access to the score manager so that we can stop the score from moving once the game is paused.
    private ScoreManager scoreManager;
    // We also want a reference to our pause menu so that we can bring up the pause menu when the button is clicked.
    public GameObject pauseMenu;

    //get a reference to the pause button so that we can disable it while we are paused.
    public GameObject pauseButton;

    public bool buttonClicked = false;


    public void Start()
    {
        // Find the score manager within the game.
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // When we hit pause, we want to stop the time scale and set the pause menu to be active.
    public void pauseGame()
    {
        buttonClicked = true;
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
        buttonClicked = false;
        pauseButton.SetActive(false);
    }

    // When we hit resume, we want time to be restored to normal and to deactivate the pause menu.
    public void resumeGame()
    {
        buttonClicked = true;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        buttonClicked = false;
        pauseButton.SetActive(true);
    }

    // When we hit Restart game, set the time scale back to normal, restart the game and set the pause menu to be inactive.
    public void RestartGame()
    {
        buttonClicked = true;
        Time.timeScale = 1.0f;
        FindObjectOfType<GameManagement>().Reset();
        pauseMenu.SetActive(false);
        buttonClicked = false;
        pauseButton.SetActive(true);
    }

    // When we hit the main menu, set the time back to normal and load the main menu scene.
    public void GoToMain()
    {
        buttonClicked = true;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(mainMenuName);
        buttonClicked = false;
    }
}

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

    public void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }


    public void pauseGame()
    {
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);

    }


    public void resumeGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        FindObjectOfType<GameManagement>().Reset();
        pauseMenu.SetActive(false);
    }

    public void GoToMain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(mainMenuName);
    }
}

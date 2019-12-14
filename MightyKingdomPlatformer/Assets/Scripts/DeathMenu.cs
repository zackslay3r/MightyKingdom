using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public string mainMenuTitle;
    


    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuTitle);
   
    }


    public void Restart()
    {
        FindObjectOfType<GameManagement>().Reset();
        
    }
}

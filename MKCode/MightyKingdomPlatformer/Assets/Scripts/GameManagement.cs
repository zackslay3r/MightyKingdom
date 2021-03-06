﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    //Store what the platform generator is, as well as its spawn point.
    public Transform platformGenerator;
    private Vector3 platformStartLocation;
    // We also need to store what the player object is, as well as its starting location.
    public PlayerMovement player;
    private Vector3 spawnPoint;

    
    private objectRemover[] platformList;


    // We need to have a reference to the score manager
    // So that we can stop the score increaing when we die.
    private ScoreManager scoreManager;

    // This is a reference to the death screen within the game.
    public DeathMenu deathScreen;

    // This is a boolean that checks if we should reset the powerups.
    public bool powerUpReset;

    //Get a reference to the power up manager so that the score per second can be fixed.
    public PowerUpManager puManager;

    //Get a reference to the pause button so that in case the player dies, we can hide the pause button.
    public GameObject pauseButton;

    // Get a reference to the platform creator so that we can reset the maximum height point upon death.
    public PlatformCreator platformMaker;

    // Start is called before the first frame update
    void Awake()
    {
        

        platformStartLocation = platformGenerator.position;
        spawnPoint = player.transform.position;

        //set score manager to be that of the the object type "ScoreManager".
        scoreManager = FindObjectOfType<ScoreManager>();
        
        // once the game starts, allow the player to start recieving score.
        scoreManager.increaseScore = true;
    }



    public void RestartGame()
    {
       
        // disable the player so that no actions can take place while we are respawning.
        player.gameObject.SetActive(false);

        //stop the player from receiving score.
        scoreManager.increaseScore = false;

        deathScreen.gameObject.SetActive(true);

        // Set the timers to 0 for each of the powerups. disabling them immediately.
        puManager.safeModeTimer = 0f;
        puManager.doublePointsTimer = 0f;

        
        scoreManager.shouldDouble = false;
        scoreManager.scorePerSecond = puManager.normalPointsPerSecond;
        //Disable the pause button.
        pauseButton.SetActive(false);

        platformMaker.maximumHeightChange = puManager.maximumHeightOriginal;
        puManager.levelTimer = 0f;
        puManager.levelUI.SetActive(false);

    }
    public void Reset()
    {
        //set the death screen to be false when we restart the game.
        deathScreen.gameObject.SetActive(false);

        // When the player dies, set all the platforms that have been generated to inactive.
        platformList = FindObjectsOfType<objectRemover>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }


        // when this coroutine is called, we want to just reset the position of the player and the platform generator.
        player.transform.position = spawnPoint;
        platformGenerator.position = platformStartLocation;

        // turn the player back on once the resetting has finished.
        player.gameObject.SetActive(true);

        // Reset the score. and allow the player to have his score increase again once the score is reset.
        scoreManager.score = 0.0f;
        scoreManager.increaseScore = true;
        // Allow the power up to be reset.
        powerUpReset = true;
        // Once the game has been reset, Set the pause button up again.
        pauseButton.SetActive(true);
      
    }

}

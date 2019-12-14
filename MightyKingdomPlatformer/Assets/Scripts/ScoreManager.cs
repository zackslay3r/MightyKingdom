using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // These are the two UI elemements that are within our Canvas.
    public Text scoreText;
    public Text highScoreText;

    //These are the two values that will be used in conjunction with the two text elements above.
    public float score;
    public float highScore;

    // We also need a variable for the points per second that we would like to give the player for each second they are alive.
    public float scorePerSecond;

    // We also want a boolean for when the player dies, stop increasing the score.
    public bool increaseScore;

    //If we have the double point powerup, we should double the amount of score we get from coins too
    public bool shouldDouble;


    // When we start the game, check to see if we have a high score value in our playerPref
    // If we do, set the high score to be that of the high score stored.
    void Start()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetFloat("highScore");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (increaseScore)
        {
            //update the score as the player plays the game.
            score += scorePerSecond * Time.deltaTime;
        }

        // If the score is ever above the high score when we play, dynamically change it to be that of the score.
        if (score > highScore)
        {
            highScore = score;
            // We want to be able to permanently set the high score within the game, 
            // So we will set the highScore within a PlayerPref as well.
            PlayerPrefs.SetFloat("highScore", highScore);
        }

        scoreText.text = "Score: " + Mathf.Round(score);
        highScoreText.text = "High Score: " + Mathf.Round(highScore);
    }


    public void AddScore(int scoreToAdd)
    {
        if (shouldDouble)
        {
            scoreToAdd = scoreToAdd * 2;
        }
        score += scoreToAdd;
    }

}

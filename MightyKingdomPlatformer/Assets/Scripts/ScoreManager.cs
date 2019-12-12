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


    // Update is called once per frame
    void Update()
    {
        if (score > highScore)
        {
            highScore = score;
        }

        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }


}

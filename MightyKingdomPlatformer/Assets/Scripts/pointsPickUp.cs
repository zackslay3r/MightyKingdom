using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointsPickUp : MonoBehaviour
{
    //This is the amount of score that we want to add to the player.
    public int scoreAmount;

    private ScoreManager scoreManager;



    // Start is called before the first frame update
    void Start()
    {
        //ensure that the score manager is assigned to the one within the game.
        scoreManager = FindObjectOfType<ScoreManager>();

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //If we collide with the player, add the score amount that is required, then disable the coin.
        if (other.gameObject.name == "Player")
        {
            scoreManager.AddScore(scoreAmount);
            gameObject.SetActive(false);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointsPickUp : MonoBehaviour
{
    //This is the amount of score that we want to add to the player.
    public int scoreAmount;

    private ScoreManager scoreManager;

    // get a reference to coin sound to play the sound when a coin is picked up.
    private AudioSource coinSound;


    // Start is called before the first frame update
    void Start()
    {
        //ensure that the score manager is assigned to the one within the game.
        scoreManager = FindObjectOfType<ScoreManager>();

        coinSound = GameObject.Find("SoundCoin").GetComponent<AudioSource>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //If we collide with the player, add the score amount that is required, then disable the coin.
        if (other.gameObject.name == "Player")
        {
            scoreManager.AddScore(scoreAmount);
            gameObject.SetActive(false);

            // if we collect multiple coins at once, have the sound play again and not overlap.
            if (coinSound.isPlaying)
            {
                coinSound.Stop();
                coinSound.Play();
            }
            else
            {
                coinSound.Play();
            }

        }

    }

 
}

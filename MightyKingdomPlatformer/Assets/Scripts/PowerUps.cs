using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    // For each of our powerups, have a bool.
    public bool doublePoints;
    public bool safeMode;
    public bool slowDown;

    // have a time for all the powerups.
    public float timer;

    // Have a reference for the pick up manager.
    private PowerUpManager puManager;

    // Have an array of sprites to choose for powerups.
    public Sprite[] pickUpSprites;

  

    // Start is called before the first frame update
    void Start()
    {
        // find the pick up manager within the game.
        puManager = FindObjectOfType<PowerUpManager>();
    }

    void Awake()
    {
        // Select a random pick up.
        int powerUpIndex = Random.Range(0, pickUpSprites.Length);

        // based on what pick up is selected, define if it will give the player extra points or make the level safer.
        // Also change the spire to reflect this.
        switch (powerUpIndex)
        {
                case 0: 
                doublePoints = true;
                GetComponent<SpriteRenderer>().sprite = pickUpSprites[0];
                break;
                case 1:
                safeMode = true;
                GetComponent<SpriteRenderer>().sprite = pickUpSprites[1];
                break;
                case 2:
                slowDown = true;
                GetComponent<SpriteRenderer>().sprite = pickUpSprites[2];
                break;


        
        }
        
    }
    // When the player collides with a powerup, activate it and tell the pick up manager what type of powerup it is and how long the ability should last.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            puManager.ActivatePowerup(doublePoints, safeMode,slowDown, timer);
         
        
        }
        
        // then we should disable the pickup.
         gameObject.SetActive(false);
    }

}

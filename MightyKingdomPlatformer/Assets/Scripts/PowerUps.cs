using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    // For each of our powerups, have a bool.
    public bool doublePoints;
    public bool safeMode;

    // have a time for all the powerups.
    public float timer;

    private PowerUpManager puManager;

    public Sprite[] pickUpSprites;

    // Start is called before the first frame update
    void Start()
    {
        puManager = FindObjectOfType<PowerUpManager>();
    }

    void Awake()
    {
        int powerUpIndex = Random.Range(0, pickUpSprites.Length);

        switch (powerUpIndex)
        {
                case 0: 
                doublePoints = true;
                GetComponent<SpriteRenderer>().sprite = pickUpSprites[0];
                break;
                case 1: safeMode = true;
                GetComponent<SpriteRenderer>().sprite = pickUpSprites[1];
                break;


        
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            puManager.ActivatePowerup(doublePoints, safeMode, timer);
        }
        gameObject.SetActive(false);
    }

}

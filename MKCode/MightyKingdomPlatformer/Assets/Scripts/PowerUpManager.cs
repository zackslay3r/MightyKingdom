using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    // For each of our powerups, have a bool.
    private bool doublePoints;
    private bool safeMode;
    private bool slow;
    private bool level;

    // Have a int for the amount of score to be added via the gem.
    public int gemAmount;


    // determine if the powerup is active.
    private bool noSpikesCurrent;
    private bool doublePointsCurrent;
    private bool levelCurrent;

    // This is a time for all the powerups.
    public float timeForPowerup;

    // This is reference to both the score and platform managers so that we are able to access data.
    private ScoreManager scoreManagement;
    public float normalPointsPerSecond;

    // This is a reference to the original maximum height value that we will be changing when the player pick's up the level pickup.
   public float maximumHeightOriginal;

    // This is reference to the platform generate and the spikerate. 
    // this is so we can remove the spikes, as well as reset the spike rate.
    private PlatformCreator platformGenerator;
    private float spikeRate;
    
    // This is reference to the game manager so that we can have access to resetting the powerups.
    private GameManagement gameManager;

    // This is an array of all the spikes within the game so that they can all be deactivated at the same time.
    private objectRemover[] spikeList;


    // These are the audio clips for the power ups.
    public AudioSource noSpikesSound;
    public AudioSource doublePointsSound;
    public AudioSource slowSound;
    public AudioSource levelSound;
    public AudioSource gemSound;

    // We also need to get a reference to each of the powerup UI elements to get access to their timers.
    public GameObject spikeNoMore;
    public GameObject doublePoint;
    public GameObject levelUI;

    // We also need a timer for each of the powerups.
    public float safeModeTimer;
    public float doublePointsTimer;
    public float levelTimer;

    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {

        // Get these private variables assigned so that we can start to modify data for powerups.
        scoreManagement = FindObjectOfType<ScoreManager>();
        platformGenerator = FindObjectOfType<PlatformCreator>();
        gameManager = FindObjectOfType<GameManagement>();

        // get references to the original values of the score multiple and the spike remover.
        normalPointsPerSecond = scoreManagement.scorePerSecond;
        spikeRate = platformGenerator.randomSpikeGeneratePercentage;
        maximumHeightOriginal = platformGenerator.maximumHeightChange;
    }

    // Update is called once per frame
    void Update()
    {

            // If double points is active, set the score per second to be 2x and set the should double boolean to true
            if (doublePointsCurrent && doublePointsTimer > 0)
            {
                scoreManagement.scorePerSecond = normalPointsPerSecond * 2.0f;
                scoreManagement.shouldDouble = true;
                Timer doubleTimer = doublePoint.GetComponentInChildren<Timer>();

                doubleTimer.timer = Mathf.Round(doublePointsTimer -= Time.deltaTime);
            }


            //if we are in safe mode, set the random spike percentage to 0.
            if (noSpikesCurrent && safeModeTimer > 0)
            {
                // We set the spike percentage change to 0.
                platformGenerator.randomSpikeGeneratePercentage = 0.0f;
                
                // We then get a reference to the Timer component that is attached to the UI elements.
                Timer spikeTimer = spikeNoMore.GetComponentInChildren<Timer>();
                // If we sucessfully get a reference, then we want to set the timer to be that of the internal
                // timer within the powerUpManager minus deltaTime.
                if (spikeTimer)
                {
                spikeTimer.timer = Mathf.Round(safeModeTimer -= Time.deltaTime);
                }
                
            }
            // If we have the level pickup still active, then we want to simply decrement both the timers
            // of the UI element and the internal timer.
            if (levelTimer > 0f && levelCurrent)
            {
                Timer levelUITimer = levelUI.GetComponentInChildren<Timer>();
                levelUITimer.timer = Mathf.Round(levelTimer -= Time.deltaTime);
        
            }
            



    // This is for when each of the timers hit 0 for each of the powerups.


            // Once the nospikes timer is finished, we want to reset the random spike value to be what it originally was
            // then we want to set the boolean values to false, signalling that we no longer have this powerup active.
            if (safeModeTimer <= 0f && noSpikesCurrent)
            {
                platformGenerator.randomSpikeGeneratePercentage = spikeRate;
                noSpikesCurrent = false;
                spikeNoMore.SetActive(false);
            }
        // Once the doublePoints timer is finished, we want to reset the score per second value to be what it originally was
        // then we want to set the boolean values to false, signalling that we no longer have this powerup active.
        if (doublePointsTimer <= 0f && doublePointsCurrent)
            {
                doublePointsCurrent = false;
                scoreManagement.scorePerSecond = normalPointsPerSecond;
                scoreManagement.shouldDouble = false;
                doublePoint.SetActive(false);
            }
        // Once the leveler timer is finished, we want to reset the random height value to be what it originally was
        // then we want to set the boolean values to false, signalling that we no longer have this powerup active.
        if (levelCurrent && levelTimer <= 0f)
            {
            platformGenerator.maximumHeightChange = maximumHeightOriginal;
            levelUI.SetActive(false);
            levelCurrent = false;

            }

        


    }


    public void ActivatePowerup(bool points, bool spikes, bool slowDown,bool levelValue,bool gem, float time)
    {
        // recieve the values from the pickup on what type of pickup and how long the pickup lasts.
        doublePoints = points;
        safeMode = spikes;
        slow = slowDown;
        level = levelValue;
        timeForPowerup = time;


        // We then want to check to see which powerup was collected via the activate powerup function.
        // Once we know which of thses were activated, activate the visual timer

        if (safeMode)
        {
            // set the boolean values that we use to check if the powerup is active to true.
            safeModeTimer = time;
            spikeNoMore.SetActive(true);
            noSpikesCurrent = true;
            // get a reference to the UI element that is linked to the spike powerup.
            Timer spikeTimer = spikeNoMore.GetComponentInChildren<Timer>();
            // if we got the reference, then make the UI timer be that of the built in timer. 
            if (spikeTimer)
            {

                spikeTimer.timer = safeModeTimer;
            }
            //find all the spikes within the level and deactivate them.
            spikeList = FindObjectsOfType<objectRemover>();
            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains("Spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }
            // Then play the sound effect for the pickup.
            noSpikesSound.Play();
        }




        // If we picked up a double point powerup, then set double points tto be active and set the UI timer.
        if (doublePoints)
        {
            doublePointsTimer = time;
            doublePointsCurrent = true;
            doublePoint.SetActive(true);

            Timer doubleTimer = spikeNoMore.GetComponentInChildren<Timer>();
            if (doubleTimer)
            {

                doubleTimer.timer = doublePointsTimer;
            }
            // Then play the sound effect for the pickup.
            doublePointsSound.Play();
        }

        // If we picked up the slow pickup, apply the effects immediately with no timer.
        if (slow)
        {
            

            player.speed = player.speed * 0.85f;
            //player.distanceMilestone += 100f;
            player.speedMilestoneCount += 100f;
                        // Then play the sound effect for the pickup.

            slowSound.Play();
        }
        // If its the level pickup, set the timer to be active and ensure the leveler bool is active.
        if (level)
        {
            levelUI.SetActive(true);
            levelTimer = time;
            levelCurrent = true;
            platformGenerator.maximumHeightChange = 0f;
            levelSound.Play();
        }

        if (gem)
        {
            scoreManagement.AddScore(gemAmount);
            gemSound.Play();
        }

       

        
    }
  
}

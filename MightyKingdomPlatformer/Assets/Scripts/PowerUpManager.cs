using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    // For each of our powerups, have a bool.
    private bool doublePoints;
    private bool safeMode;

    // determine if the powerup is active.
    private bool powerupActive;

    // This is a time for all the powerups.
    public float timeForPowerup;

    // This is reference to both the score and platform managers so that we are able to access data.
    private ScoreManager scoreManagement;
    private float normalPointsPerSecond;

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


    // Start is called before the first frame update
    void Start()
    {

        // Get these private variables assigned so that we can start to modify data for powerups.
        scoreManagement = FindObjectOfType<ScoreManager>();
        platformGenerator = FindObjectOfType<PlatformCreator>();
        gameManager = FindObjectOfType<GameManagement>();

    }

    // Update is called once per frame
    void Update()
    {
        // If we have a powerup active, we need to ensure that the time for the powerup goes down.
        if (powerupActive)
        {
            timeForPowerup -= Time.deltaTime;

            // if the game manager wants a power up to be reset, then set the time for the powerup to 0 and set the boolean to false.
            if (gameManager.powerUpReset)
            {
                timeForPowerup = 0.0f;
                gameManager.powerUpReset = false;
            }

            // If double points is active, set the score per second to be 2x and set the should double boolean to true
            if (doublePoints)
            {
                scoreManagement.scorePerSecond = normalPointsPerSecond * 2.0f;
                scoreManagement.shouldDouble = true;
            }
            //if we are in safe mode, set the random spike percentage to 0.
            if (safeMode)
            {
                 platformGenerator.randomSpikeGeneratePercentage = 0.0f;
            }

            // Once the time reaches 0, turn off the powerup and reset all values.
            if (timeForPowerup <= 0)
            {
                powerupActive = false;
                scoreManagement.scorePerSecond = normalPointsPerSecond;
                scoreManagement.shouldDouble = false;
                platformGenerator.randomSpikeGeneratePercentage = spikeRate;
            }
        
        }
    }


    public void ActivatePowerup(bool points, bool spikes, float time)
    {
        // recieve the values from the pickup on what type of pickup and how long the pickup lasts.
        doublePoints = points;
        safeMode = spikes;
        timeForPowerup = time;

        // get references to the original values of the score multiple and the spike remover.
        normalPointsPerSecond = scoreManagement.scorePerSecond;
        spikeRate = platformGenerator.randomSpikeGeneratePercentage;

        // If we are in safe mode now, find all the spikes within the level and deactivate them.
        if (safeMode)
        {
            spikeList = FindObjectsOfType<objectRemover>();
            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains("Spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }
        }

        // Depending on which pickup it is, play the corresponding sound.
        if (doublePoints)
        {
             doublePointsSound.Play();
        }
        if (safeMode)
        {
            noSpikesSound.Play();
        }
        // then, set the powerup to be active.
        powerupActive = true;


        
    }
  
}

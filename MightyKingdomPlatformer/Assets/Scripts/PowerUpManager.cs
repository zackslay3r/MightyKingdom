using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    // For each of our powerups, have a bool.
    private bool doublePoints;
    private bool safeMode;


    private bool powerupActive;

    // have a time for all the powerups.
    public float timeForPowerup;

    // Have a reference to both the score and platform managers so that we are able to access data.
    private ScoreManager scoreManagement;
    private float normalPointsPerSecond;

    private PlatformCreator platformGenerator;
    private float spikeRate;
    
    private GameManagement gameManager;

    private objectRemover[] spikeList;


   
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
        if (powerupActive)
        {
            timeForPowerup -= Time.deltaTime;

            if (gameManager.powerUpReset)
            {
                timeForPowerup = 0.0f;
                gameManager.powerUpReset = false;
            }


            if (doublePoints)
            {
                scoreManagement.scorePerSecond = normalPointsPerSecond * 2.0f;
                scoreManagement.shouldDouble = true;
            }
            if (safeMode)
            {
                platformGenerator.randomSpikeGeneratePercentage = 0.0f;
            }


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
        doublePoints = points;
        safeMode = spikes;
        timeForPowerup = time;


        normalPointsPerSecond = scoreManagement.scorePerSecond;
        spikeRate = platformGenerator.randomSpikeGeneratePercentage;

        if (safeMode)
        {
            // When the player dies, set all the platforms that have been generated to inactive.
            spikeList = FindObjectsOfType<objectRemover>();
            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains("Spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }
        }



        powerupActive = true;


        
    }
  
}

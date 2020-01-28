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

    // determine if the powerup is active.
    private bool noSpikesCurrent;
    private bool doublePointsCurrent;

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
            if (doublePointsCurrent)
            {
                scoreManagement.scorePerSecond = normalPointsPerSecond * 2.0f;
                scoreManagement.shouldDouble = true;
                Timer doubleTimer = doublePoint.GetComponentInChildren<Timer>();

                doubleTimer.timer = Mathf.Round(doublePointsTimer -= Time.deltaTime);
            }
            //if we are in safe mode, set the random spike percentage to 0.
            if (noSpikesCurrent)
            {

                    platformGenerator.randomSpikeGeneratePercentage = 0.0f;
                    Timer spikeTimer = spikeNoMore.GetComponentInChildren<Timer>();
                    if (spikeTimer)
                    {

                        spikeTimer.timer = Mathf.Round(safeModeTimer -= Time.deltaTime);

                    }
                
            }
            if (level)
            {
                Timer levelUITimer = levelUI.GetComponentInChildren<Timer>();
                levelUITimer.timer = Mathf.Round(levelTimer -= Time.deltaTime);
        
            }
            
            if (safeModeTimer <= 0f && noSpikesCurrent)
            {
                platformGenerator.randomSpikeGeneratePercentage = spikeRate;
                noSpikesCurrent = false;
                spikeNoMore.SetActive(false);
            }
            if (doublePointsTimer <= 0f && doublePointsCurrent)
            {
                doublePointsCurrent = false;
                scoreManagement.scorePerSecond = normalPointsPerSecond;
                scoreManagement.shouldDouble = false;
                doublePoint.SetActive(false);
            }
            if (level && levelTimer <= 0f)
            {
            platformGenerator.maximumHeightChange = maximumHeightOriginal;
            levelUI.SetActive(false);
            level = false;
            }

        // }


    }


    public void ActivatePowerup(bool points, bool spikes, bool slowDown,bool levelValue, float time)
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
            safeModeTimer = time;
            spikeNoMore.SetActive(true);
            noSpikesCurrent = true;
            Timer spikeTimer = spikeNoMore.GetComponentInChildren<Timer>();
            if (spikeTimer)
            {

                spikeTimer.timer = safeModeTimer;
            }

        }



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
        }


        if (slow)
        {
            

            player.speed = player.speed * 0.85f;
            player.distanceMilestone = ((player.distanceMilestone + player.speedMultiplier) * 0.8f);
            player.speedMilestoneCount += player.distanceMilestone;
            slowSound.Play();
        }

        if (level)
        {
            levelUI.SetActive(true);
            levelTimer = time;
            platformGenerator.maximumHeightChange = 0f;
        }


        //// Depending on which pickup it is, play the corresponding sound.
        if (doublePoints)
        {
            doublePointsSound.Play();
            
        }
    
        if (safeMode)
        {
            noSpikesSound.Play();
        }
        // then, set the powerup to be active.
        //powerupActive = true;


        
    }
  
}

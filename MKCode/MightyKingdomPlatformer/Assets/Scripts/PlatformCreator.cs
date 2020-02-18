using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    // This is where we generate platforms from.
    public Transform generationPoint;
    // This is the distance between platforms.
    public float distanceBetween;

    // These are our object pools that contain platforms.
    //public ObjectPoolScript[] ObjectPools;


  
    // This is the int that we use for selecting a platform to activate.
    private int platformIndex;
    //This stores the width of all platforms.
    private float[] platformWidths;

    // Two random floats that are able to determine the widith between platforms
    public float pDistanceMin, pDistanceMax;


    // This is the minimum and maximum height that we can spawn our platforms at, as well as an overall maximum for height of platforms.
    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    // This is the maximum that platform height can change.
    public float maximumHeightChange;
    private float heightChange;


    //get a reference to the coin generator.
    private CoinCreator coinCreator;
    // create a number that will act as the random percentage for coins to spawn.
    public float randomCoinGeneratePercentage;

    //And have the object poll reference of our spikes.
    //public ObjectPoolScript spikePool;
    //Have a random spike generator percentage.
    public float randomSpikeGeneratePercentage;

    //This contains the pickup height, as well as the object pool for the powerups and the powerUp percentage chance.
    public float pickupHeight;
    //public ObjectPoolScript powerUpPool;
    public float powerUpPercentage;


    /** NEW CODE **/
    // We will now have a reference to a global object pool.
    public GlobalPooler gp;


    // Start is called before the first frame update
    void Awake()
    {
        // Find our coin creator.
        coinCreator = FindObjectOfType<CoinCreator>();

        // Get the length of the platform that we are about to generate.

        // we will first need to grab our global object pool and see if they are a platform.
        // we can do this by determining if they have a box collider or not. 
        // if they do, we are going to increment an int that we will then use in determining platform length.

        int platformAmount = 0;
        gp.CreatePool();
        for (int i = 0; i < gp.pooledWorldObjects.Count; i++)
        {
            if (gp.pooledWorldObjects[i].GetComponent<BoxCollider2D>())
            {
                platformAmount++;
            }
        }
        platformWidths = new float[platformAmount];




        // For every object in our platform object pools, we are going to set the corresponding array index
        // within 'platformWidths' to be that of the platform we are looking at.
        for (int i = 0; i < gp.pooledWorldObjects.Count; i++)
        {
            if (gp.pooledWorldObjects[i].tag != "KillVolume" && gp.pooledWorldObjects[i].tag != "Coin" && gp.pooledWorldObjects[i].tag != "PowerUp")
            {
                platformWidths[i] = gp.pooledWorldObjects[i].GetComponent<BoxCollider2D>().size.x;
            }
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
       
    }

    // Update is called once per frame
    void Update()
    {
        // If the position of our platform is going to be less then that of the generation point
        // we want to select a random distance between, as well as a platform index.
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(pDistanceMin, pDistanceMax);

            // This is the amount of different platforms we are using.
            List<GameObject> platforms = gp.getDifferentPlatforms();

            platformIndex = Random.Range(0, platforms.Count);

            // We then want to calculate a height differental for our platform.
            heightChange = transform.position.y + Random.Range(maximumHeightChange, -maximumHeightChange);
            // Though we should ignore it if it goes beyond the overall maximum height.
            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            // as well as if it goes below the minimum height set.
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            // We then want to get a random number between 0 and 100
            // if it is less then the power up percentage, make a new powerup.
            if (Random.Range(0.0f, 100.0f) < powerUpPercentage)
            {
                GameObject newPowerup = gp.getPooledObject("PowerUp");

                // This powerup will be in between 2 platforms, with a random height difference.
                newPowerup.transform.position = transform.position + new Vector3(distanceBetween / 2.0f, Random.Range(2.0f,pickupHeight), 0.0f);
                // Then, set the powerup to be active.
                newPowerup.SetActive(true);
            }
            // We then want to calculate our new position.
            transform.position = new Vector3(transform.position.x + (platformWidths[platformIndex] / 2) + distanceBetween, heightChange, transform.position.z);

            
            
           // We then get a pooled platform and set its position and roation to be that of our platform creator.
            GameObject newPlatform = gp.getPooledObject(platforms[platformIndex].tag);
            
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            // then set it to be active.
            newPlatform.SetActive(true);


            // Based on the percentage, if we generate a number that is less then the percentage number, create the coins.
            if (Random.Range(0.0f, 100.0f) < randomCoinGeneratePercentage)
            {

                // Add the coins to the platform.
                coinCreator.GenerateCoins(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z));
            }

            // Based on the percentage, if we generate a number that is less then the percentage number, create the spikes.
            if (Random.Range(0.0f, 100.0f) < randomSpikeGeneratePercentage)
            {
                // grab the spike from the spike object pool.
                GameObject newSpike = gp.getPooledObject("KillVolume");
                //These spikes will have a random position on the platform. based on this random x coordinate.
                float spikeXPos = Random.Range(-platformWidths[platformIndex] / 2 + 1.0f, platformWidths[platformIndex] / 2 - 1.0f);


                // add our x position differental to our spikes position, with the height being 0.5f to ensure its on top of the platform.
                Vector3 spikePos = new Vector3(spikeXPos, 0.5f, 0f);
                // Set the new spikes position and rotation to be that of the platform creator with the addition of the spikePos.
                newSpike.transform.position = transform.position + spikePos;
                newSpike.transform.rotation = transform.rotation;
                // Then we want to set the spikes to be active.
                newSpike.SetActive(true);
            }

            // After all this, jump the platform generator forward to the next position within the game world.
            transform.position = new Vector3(transform.position.x + (platformWidths[platformIndex] / 2), transform.position.y, transform.position.z);


        }
    }
}

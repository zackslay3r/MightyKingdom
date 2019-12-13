using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    //public GameObject[] platforms;
    public Transform generationPoint;
    public float distanceBetween;

    public ObjectPoolScript[] ObjectPools;

    private float platformWidth;

    private int platformIndex;
    private float[] platformWidths;

    // Two random floats that are able to determine the widith between platforms
    public float pDistanceMin, pDistanceMax;



    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;

    public float maximumHeightChange;
    private float heightChange;


    //get a reference to the coin generator.
    private CoinCreator coinCreator;
    // create a number that will act as the random percentage for coins to spawn.
    public float randomCoinGeneratePercentage;

    // Start is called before the first frame update
    void Start()
    {

        coinCreator = FindObjectOfType<CoinCreator>();

        // Get the length of the platform that we are about to generate.
        //platformWidth = newPlatform.GetComponent<BoxCollider2D>().size.x;
        platformWidths = new float[ObjectPools.Length];

        for (int i = 0; i < ObjectPools.Length; i++)
        {
            platformWidths[i] = ObjectPools[i].objectToPool.GetComponent<BoxCollider2D>().size.x;

        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(pDistanceMin, pDistanceMax);
            
            platformIndex = Random.Range(0, ObjectPools.Length);


            heightChange = transform.position.y + Random.Range(maximumHeightChange, -maximumHeightChange);

            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }


            transform.position = new Vector3(transform.position.x + (platformWidths[platformIndex] / 2) + distanceBetween, heightChange, transform.position.z);

            
            
            //Instantiate(platforms[platformIndex], transform.position, transform.rotation);
            GameObject newPlatform = ObjectPools[platformIndex].getPooledObject();
            
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if (Random.Range(0.0f, 100.0f) < randomCoinGeneratePercentage)
            {
                
                
                
                
                // Add the coins to the platform.
                coinCreator.GenerateCoins(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z));
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformIndex] / 2), transform.position.y, transform.position.z);


        }
    }
}

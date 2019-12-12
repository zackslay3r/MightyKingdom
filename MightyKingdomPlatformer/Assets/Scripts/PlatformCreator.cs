using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    public GameObject[] platforms;
    public Transform generationPoint;
    public float distanceBetween;

    //public ObjectPoolScript theObjectPool;

    private float platformWidth;

    private int platformIndex;
    private float[] platformWidths;

    // Two random floats that are able to determine the widith between platforms
    public float pDistanceMin, pDistanceMax;


    // Start is called before the first frame update
    void Start()
    {
        // Get the length of the platform that we are about to generate.
        //platformWidth = newPlatform.GetComponent<BoxCollider2D>().size.x;
        platformWidths = new float[platforms.Length];

        for (int i = 0; i < platforms.Length; i++)
        {
            platformWidths[i] = platforms[i].GetComponent<BoxCollider2D>().size.x;

        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(pDistanceMin, pDistanceMax);
            
            platformIndex = Random.Range(0, platforms.Length);
            
            transform.position = new Vector3(transform.position.x + platformWidths[platformIndex] + distanceBetween, transform.position.y, transform.position.z);

            
            
            Instantiate(platforms[platformIndex], transform.position, transform.rotation);
            //GameObject newPlatform = theObjectPool.getPooledObject();
            /*
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
            */
        }
    }
}

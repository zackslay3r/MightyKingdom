using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPooler : MonoBehaviour
{
    // This will store all of our global pooled objects.
    public List<GameObject> pooledWorldObjects;

    // This is all of the objects within the object 
    public List<GameObject> objectsToPool;

    public int platform5x1Amount;
    public int platform7x1Amount;
    public int coinAmount;
    public int powerUpAmount;
    public int spikeAmount;






    public GameObject getPooledObject(string tag)
    {
        // For all the pooled objects, if they are not active in the hierarchy, return the first non-active one.
        for (int i = 0; i < pooledWorldObjects.Count; i++)
        {
            if (!pooledWorldObjects[i].activeInHierarchy && pooledWorldObjects[i].tag == tag)
            {
                return pooledWorldObjects[i];
            }
        }
        // Else, we are going to create a new one and then immediately use this one, since the pool is busy.
        GameObject obj = AddToBusyPool(tag);
        return obj;
    }

    public void CreatePool()
    {
        // We will first start off with an empty list for out object pool. 
        pooledWorldObjects = new List<GameObject>();


        // We then need to loop though the list of objects to be pooled and based on what they are
        // add an amount of these objects to the world pool. 
        if (objectsToPool.Count > 0)
        {
            for (int i = 0; i < objectsToPool.Count; i++)
            {
                switch (objectsToPool[i].tag)
                {
                    // If it is the 5x1 platform..
                    case "Platform5x1":
                        for (int j = 0; j < platform5x1Amount; j++)
                        {
                            GameObject obj = Instantiate(objectsToPool[i]);
                            obj.SetActive(false);
                            pooledWorldObjects.Add(obj);
                        }
                        break;
                    // If it is the 7x1 platform..
                    case "Platform7x1":
                        for (int j = 0; j < platform7x1Amount; j++)
                        {
                            GameObject obj = Instantiate(objectsToPool[i]);
                            obj.SetActive(false);
                            pooledWorldObjects.Add(obj);
                        }
                        break;
                    // If it is the 9x1 platform..
                    case "Platform9x1":
                        for (int j = 0; j < platform9x1Amount; j++)
                        {
                            GameObject obj = Instantiate(objectsToPool[i]);
                            obj.SetActive(false);
                            pooledWorldObjects.Add(obj);
                        }
                        break;
                    // If it is the PowerUp..
                    case "PowerUp":
                        for (int j = 0; j < powerUpAmount; j++)
                        {
                            GameObject obj = Instantiate(objectsToPool[i]);
                            obj.SetActive(false);
                            pooledWorldObjects.Add(obj);
                        }
                        break;
                    // If it is the coins...
                    case "Coin":
                        for (int j = 0; j < coinAmount; j++)
                        {
                            GameObject obj = Instantiate(objectsToPool[i]);
                            obj.SetActive(false);
                            pooledWorldObjects.Add(obj);
                        }
                        break;
                    // If it is the Spikes..
                    case "KillVolume":
                        for (int j = 0; j < spikeAmount; j++)
                        {
                            GameObject obj = Instantiate(objectsToPool[i]);
                            obj.SetActive(false);
                            pooledWorldObjects.Add(obj);
                        }
                        break;
                    default:
                        break;
                }
            }

        }

    }
    // Update is called once per frame
    void Update()
    {

    }


    void AddToPool(string name)
    {
        for (int i = 0; i < objectsToPool.Count; i++)
        {
            if (objectsToPool[i].tag == name)
            {
                //cast to a gameObject, ensuring that we are Instantiating a new gameObject.
                GameObject obj = (GameObject)Instantiate(objectsToPool[i]);
                //ensure that it is not active within the scene.
                obj.SetActive(false);
                //Add to the list of pooled objects.
                pooledWorldObjects.Add(obj);
            }
        }
    }

    GameObject AddToBusyPool(string name)
    {
        GameObject obj = new GameObject();
        for (int i = 0; i < objectsToPool.Count; i++)
        {
            if (objectsToPool[i].tag == name)
            {
                //cast to a gameObject, ensuring that we are Instantiating a new gameObject.
                obj = (GameObject)Instantiate(objectsToPool[i]);
                //ensure that it is not active within the scene.
                obj.SetActive(false);
                //Add to the list of pooled objects.
                pooledWorldObjects.Add(obj);
                
            }
        }
        return obj;

    }
    // We are going to return a list of the different platform types as this will be used in the retreval of platform objects.
    public List<GameObject> getDifferentPlatforms()
    {

        List<GameObject> platforms = new List<GameObject>();

        string currentTag = "";
        // we are going to loop though the amount of pooled object and based on how many different platforms there are, we will return the different amount.
        for (int i = 0; i < pooledWorldObjects.Count; i++)
        {
            // if any of the objects is a platform, we are then going to first add the first platform we see into the list as a type of platform.
            // Then for use as a comparer, we will store the items tag for use in comparing all platforms. 
            if (pooledWorldObjects[i].GetComponent<BoxCollider2D>() && pooledWorldObjects[i].tag != "KillVolume")
            {
                // If we have not yet recieved a platform to compare against, make it the default.
                if (currentTag == "")
                {
                    currentTag = pooledWorldObjects[i].tag;
                    platforms.Add(pooledWorldObjects[i]);
                }
                //If we have, then we are going to compare all the platforms against each other to confirm the amount of different platform types. 
                else
                {
                    if (pooledWorldObjects[i].tag != currentTag)
                    {
                        currentTag = pooledWorldObjects[i].tag;
                        platforms.Add(pooledWorldObjects[i]);
                    }
                }
            }
        }

        return platforms;
    }

}

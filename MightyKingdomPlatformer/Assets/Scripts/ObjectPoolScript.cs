﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolScript : MonoBehaviour
{
    public GameObject objectToPool;

    public int poolAmount;

    public List<GameObject> pooledObjects;



    // Start is called before the first frame update
    void Awake()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < poolAmount; i++)
        {
            //cast to a gameObject, ensuring that we are Instantiating a new gameObject.
            GameObject obj = (GameObject)Instantiate(objectToPool);
            //ensure that it is not active within the scene.
            obj.SetActive(false);
            //Add to the list of pooled objects.
            pooledObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject getPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        //cast to a gameObject, ensuring that we are Instantiating a new gameObject.
        GameObject obj = (GameObject)Instantiate(objectToPool);
        //ensure that it is not active within the scene.
        obj.SetActive(false);
        //Add to the list of pooled objects.
        pooledObjects.Add(obj);
        return obj;

    }

}

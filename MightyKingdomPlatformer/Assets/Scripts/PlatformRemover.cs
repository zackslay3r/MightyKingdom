using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRemover : MonoBehaviour
{
    public GameObject platformDestroyPoint;


    // Start is called before the first frame update
    void Start()
    {
        // Find the destroy point of which we will delete platforms from.
        platformDestroyPoint = GameObject.Find("DestroyPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < platformDestroyPoint.transform.position.x)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}

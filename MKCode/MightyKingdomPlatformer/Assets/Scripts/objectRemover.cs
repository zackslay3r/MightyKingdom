using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectRemover : MonoBehaviour
{
    public GameObject objectDestroyPoint;


    // Start is called before the first frame update
    void Start()
    {
        // Find the destroy point of which we will delete platforms from.
        objectDestroyPoint = GameObject.Find("DestroyPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < objectDestroyPoint.transform.position.x)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}

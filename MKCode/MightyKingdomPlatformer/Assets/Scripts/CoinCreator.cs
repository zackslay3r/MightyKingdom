using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCreator : MonoBehaviour
{

    // Get a reference to our object.
    public GlobalPooler pooler;

    // This is the distance between all coins.
    public float distanceBetweenCoins;


    public void GenerateCoins(Vector3 positionStart)
    {
        
        // get a coin object and place it at the vector 3 location given an set it as active.
        GameObject coin1 = pooler.getPooledObject("Coin");
        coin1.transform.position = positionStart;
        coin1.SetActive(true);
        // create another coin and place to to the left on the center coin by the value distanceBetweenCoins.
        GameObject coin2 = pooler.getPooledObject("Coin");
        coin2.transform.position = new Vector3(positionStart.x - distanceBetweenCoins, positionStart.y, positionStart.z);
        coin2.SetActive(true);
        // create another coin and place to to the right on the center coin by the value distanceBetweenCoins.
        GameObject coin3 = pooler.getPooledObject("Coin");
        coin3.transform.position = new Vector3(positionStart.x + distanceBetweenCoins, positionStart.y, positionStart.z);
        coin3.SetActive(true);

    }
}

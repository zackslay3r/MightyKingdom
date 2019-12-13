using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCreator : MonoBehaviour
{

    public ObjectPoolScript coinPool;

    public float distanceBetweenCoins;


    public void GenerateCoins(Vector3 positionStart)
    {
        int RandomAmountofCoins = Random.Range(1, 3);

        float randomDistanceChange = Random.Range(-0.5f, 0.5f);




        
            GameObject coin1 = coinPool.getPooledObject();
            coin1.transform.position = positionStart;
            coin1.SetActive(true);
        
        GameObject coin2 = coinPool.getPooledObject();
        coin2.transform.position = new Vector3(positionStart.x - distanceBetweenCoins, positionStart.y, positionStart.z);
        coin2.SetActive(true);

        GameObject coin3 = coinPool.getPooledObject();
        coin3.transform.position = new Vector3(positionStart.x + distanceBetweenCoins, positionStart.y, positionStart.z);
        coin3.SetActive(true);

    }
}

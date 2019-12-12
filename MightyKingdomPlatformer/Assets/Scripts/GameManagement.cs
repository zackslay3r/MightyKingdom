using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    //Store what the platform generator is, as well as its spawn point.
    public Transform platformGenerator;
    private Vector3 platformStartLocation;
    // We also need to store what the player object is, as well as its starting location.
    public PlayerMovement player;
    private Vector3 spawnPoint;

    private PlatformRemover[] platformList;


    // Start is called before the first frame update
    void Start()
    {
        platformStartLocation = platformGenerator.position;
        spawnPoint = player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartGame()
    {
        StartCoroutine("RestartingGameCoroutine");
    }


    public IEnumerator RestartingGameCoroutine()
    {
        // disable the player so that no actions can take place while we are respawning.
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        // When the player dies, set all the platforms that have been generated to inactive.
        platformList = FindObjectsOfType<PlatformRemover>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        
        
        // when this coroutine is called, we want to just reset the position of the player and the platform generator.
        player.transform.position = spawnPoint;
        platformGenerator.position = platformStartLocation;
        // turn the player back on once the resetting has finished.
        player.gameObject.SetActive(true);

        
       

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is responsible for playing the background music, regardless on whether or not we are in the main menu
// or the game itself.
public class BackgroundAudio : MonoBehaviour
{
   
    private static BackgroundAudio instance = null;
    public static BackgroundAudio Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}

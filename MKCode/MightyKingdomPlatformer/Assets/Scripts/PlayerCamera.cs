using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // This is a reference to the player movement script.
    private PlayerMovement player;
    // This is used to keep track of the last known player position.
    private Vector3 lastPlayerPos;
    // This is used for tracking the amount of distance the player needs to move.
    private float distanceToMoveCamera;


    // Start is called before the first frame update
    void Start()
    {
        // Set the player movement reference to be that of the player movement script attached to the player.
        player = FindObjectOfType<PlayerMovement>();

        // this value is the position the player was at the first frame.
        lastPlayerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // distanceToMoveCamera is now equal to the amount the player has moved in a frame.
        distanceToMoveCamera = player.transform.position.x - lastPlayerPos.x;
        // make the camera's position that of the current position of the player.
        transform.position = new Vector3(transform.position.x + distanceToMoveCamera, transform.position.y,transform.position.z);
        // set this position to be the last known player position.
        lastPlayerPos = player.transform.position;
    }
}

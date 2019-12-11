using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public PlayerMovement player;

    private Vector3 lastPlayerPos;
    private float distanceToMoveCamera;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();


        lastPlayerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        distanceToMoveCamera = player.transform.position.x - lastPlayerPos.x;

        transform.position = new Vector3(transform.position.x + distanceToMoveCamera, transform.position.y,transform.position.z);

        lastPlayerPos = player.transform.position;
    }
}

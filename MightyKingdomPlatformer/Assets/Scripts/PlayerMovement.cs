using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    private Rigidbody2D playerRb;


    
    // Start is called before the first frame update
    void Start()
    {
        //Assign the value for playerRb
        playerRb = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        // Have the player automatically move based on the speed that was predefined.
        playerRb.velocity = new Vector2(speed, playerRb.velocity.y);

        // If we hit the spacebar or tap the screen, then we want the player to jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetTouch(0).phase == TouchPhase.Began)
        {
            playerRb.velocity = new Vector2(0, jumpPower);
        }

    }
}

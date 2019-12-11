using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public bool onGround;


    private Rigidbody2D playerRb;
    private Collider2D playerCollider;
    private Animator playerAnimator;
    public LayerMask definedGround;

    
    // Start is called before the first frame update
    void Start()
    {
        //Assign the value for playerRb
        playerRb = GetComponent<Rigidbody2D>();
        // Assign the private 'playerCollider' variable.
        playerCollider = GetComponent<Collider2D>();
        // Assign the animator component
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Determine if we are on the ground by checking if we are touchiing anything of the layer 'ground'
        onGround = Physics2D.IsTouchingLayers(playerCollider, definedGround);

        // Have the player automatically move based on the speed that was predefined.
        playerRb.velocity = new Vector2(speed, playerRb.velocity.y);

        // If we hit the spacebar or tap the screen, then we want the player to jump
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetTouch(0).phase == TouchPhase.Began)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //If we are on the ground, allow the player the ability to be able to jump.
            if (onGround)
            {
                playerRb.velocity = new Vector2(0, jumpPower);
            }
        }


        playerAnimator.SetFloat("Speed", playerRb.velocity.x);
        playerAnimator.SetBool("Grounded", onGround);

    }
}

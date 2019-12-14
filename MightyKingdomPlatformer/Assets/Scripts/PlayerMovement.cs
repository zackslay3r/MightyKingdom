using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    // store the inital speed for when the player needs to respawn.
    private float storedSpeed;


    // This variable is responsible for being the multiplier of the player's speed once the player has reached a certain distance
    // This is to be able to add some difficulty to the game.
    public float speedMultiplier;


    // These are responsible for the creation of milestones that, once the player has acheieved, will alter to new goals.
    // The distance milestone is the length from the current milestone till the next milestone until the speed is increased.
    // The speedMilestoneCount is the total length that would've needed to have been travelled in order to get to the next milestone.
    public float distanceMilestone;
    private float speedMilestoneCount;

    // This will be used when the game restarts, setting the player milesstones to the original values.
    private float speedMilestoneCountStored;

    // Also we want to store the original distanceMilestone.
    private float distanceMilestoneStored;

    public float jumpPower;
    public bool onGround;


    public float jumpTime;
    private float jumpTimeCounter;

    private Rigidbody2D playerRb;
    //private Collider2D playerCollider;
    private Animator playerAnimator;

    public Transform groundCheck;
    public float checkerSize;


    public LayerMask definedGround;


    public GameManagement gameManager;


    private bool stoppedJumping;
    private bool canDoubleJump;


    public bool inSky;

    //have a private variable that will get the death screen within the game.
    public GameObject deathScreen;

    public GameObject pauseScreen;


    // Get the references for the two sounds that are within the game.
    public AudioSource jumpSound;
    public AudioSource deathSound;


    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
        pauseScreen.SetActive(false);

        // assign the store values that will be used for when the level restarts.
        storedSpeed = speed;
        speedMilestoneCountStored = speedMilestoneCount;
        distanceMilestoneStored = distanceMilestone;


      


        //Assign the value for playerRb
        playerRb = GetComponent<Rigidbody2D>();
        // Assign the private 'playerCollider' variable.
        //playerCollider = GetComponent<Collider2D>();
        // Assign the animator component
        playerAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

        speedMilestoneCount = distanceMilestone;

        stoppedJumping = true;
        canDoubleJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        


        // Determine if we are on the ground by checking if we are touchiing anything of the layer 'ground'
      
        onGround = Physics2D.OverlapCircle(groundCheck.position, checkerSize, definedGround);

       
       

        if (transform.position.x > speedMilestoneCount)
        {
            speed = speed * speedMultiplier;

            distanceMilestone = distanceMilestone * speedMultiplier;
            speedMilestoneCount += distanceMilestone;
        }

        // Have the player automatically move based on the speed that was predefined.
        playerRb.velocity = new Vector2(speed, playerRb.velocity.y);

        // If we hit the spacebar or tap the screen, then we want the player to jump
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetTouch(0).phase == TouchPhase.Began)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (!onGround && canDoubleJump)
            {
                jumpTimeCounter = jumpTime;
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
                canDoubleJump = false;
                stoppedJumping = false;
                jumpSound.Play();
            }

            //If we are on the ground, allow the player the ability to be able to jump.
            if (onGround)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
                stoppedJumping = false;
                onGround = false;
                jumpSound.Play();
               
            }
            
        }
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                if (jumpTimeCounter > 0 && playerRb.velocity.y > 0)
                {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
                    inSky = true;
                    jumpTimeCounter -= Time.deltaTime;

              
                }
            }

        //Once the player has let go of the space bar, dont allow them to press the space bar again.

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;


        }
       
        playerAnimator.SetFloat("Speed", playerRb.velocity.x);
        playerAnimator.SetBool("Grounded", onGround);
          if (onGround)
            {
                jumpTimeCounter = jumpTime;
                canDoubleJump = true;
            }
       
    }

    // When we collide with the Catcher, kill the player and restart the level.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "KillVolume")
        {
           
            gameManager.RestartGame();
            
            
            //Reset the speed and the milestoneCount.
            speed = storedSpeed;
            speedMilestoneCount = speedMilestoneCountStored;
            distanceMilestone = distanceMilestoneStored;
            deathSound.Play();
        }
    }

}

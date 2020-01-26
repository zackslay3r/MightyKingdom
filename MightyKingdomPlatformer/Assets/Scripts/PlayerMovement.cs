using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // This is the inital speed of the player when the game starts.
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
    public float speedMilestoneCount;

    // This will be used when the game restarts, setting the player milesstones to the original values.
    private float speedMilestoneCountStored;

    // Also we want to store the original distanceMilestone.
    private float distanceMilestoneStored;

    // This determines the amount of power we have to jump in the game.
    public float jumpPower;
    
    // This is our boolean value to see if we are on the ground.
    public bool onGround;

    // This is our jumptime that we can hold the space bar down for.
    public float jumpTime;
    // This actively tracks the amount of time used from our jumpTime.
    private float jumpTimeCounter;

    // This is a reference to the player RigidBody.
    private Rigidbody2D playerRb;
   
    // This is a reference to the Animator on the player.
    private Animator playerAnimator;

    // This is our gameObject for checking to see if we are on ground.
    public Transform groundCheck;
    // And this is our big the radius of our circle for our groundcheck will be.
    public float checkerSize;

    // This LayerMask tells the player what is defined as ground.
    public LayerMask definedGround;

    // This is a reference to the game manager.
    public GameManagement gameManager;


    // This boolean is used to see if the player is allowed to double jump.
    private bool canDoubleJump;

    // this boolean is used to check to see if the player is in the sky.
    public bool inSky;

    //These are references for the death and pause screens of the game.
    public GameObject deathScreen;
    public GameObject pauseScreen;


    // Get the references for the two sounds that are within the game connected to the player.
    public AudioSource jumpSound;
    public AudioSource deathSound;

    // We also want to grab the PowerUp UI Elements so that when the player dies, we reset the timers and disable the icons.
    public GameObject spikeUI;
    public GameObject doubleUI;


    


    // Start is called before the first frame update
    void Start()
    {
        // Initally, set the death and pause screens to be false.
        deathScreen.SetActive(false);
        pauseScreen.SetActive(false);

        // assign the store values that will be used for when the level restarts.
        storedSpeed = speed;
        speedMilestoneCountStored = speedMilestoneCount;
        distanceMilestoneStored = distanceMilestone;


      


        //Assign the value for player rigidbody which will just be a reference to the rigidbody attached to the player.
        playerRb = GetComponent<Rigidbody2D>();
       
        // Get the animator component attached from the player.
        playerAnimator = GetComponent<Animator>();

        // Set the timer for jumptime.
        jumpTimeCounter = jumpTime;

        // Set the distance required for the distance milestone.
        speedMilestoneCount = distanceMilestone;

        // Allow the player the ability to double jump.
        canDoubleJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        


        // Determine if we are on the ground by checking if we are touchiing anything of the layer 'ground'
        onGround = Physics2D.OverlapCircle(groundCheck.position, checkerSize, definedGround);

       
       
        // If we have hit the distance milestone, increase the speed of the player by the speed multiplier
        // Then set a new distance milestone, as well as and on the dsitance milestone to the speedMilestoneCount.
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
           
            //if we are in the sky and not on the ground, and we are able to double jump, perform the 'double jump'
            if (!onGround && inSky && canDoubleJump)
            {
                jumpTimeCounter = jumpTime;
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
                // disable the double jump so the player cant jump again.
                canDoubleJump = false;
                
                jumpSound.Play();
            }

            //If we are on the ground, allow the player the ability to be able to jump.
            if (onGround)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
                
                onGround = false;
                jumpSound.Play();
               
            }
            
        }
        // If the player holds down the space or mouse button, then keep adding power to the jump
        // until the timer hits 0.
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
          


        }
       
        //Set the values of Speed and grounded within the animator to be that of the player velocity and of the 'OnGround' boolean.
        playerAnimator.SetFloat("Speed", playerRb.velocity.x);
        playerAnimator.SetBool("Grounded", onGround);
          if (onGround)
            {
                jumpTimeCounter = jumpTime;
                canDoubleJump = true;
            }
       
    }

    // When we collide with any killVolumes, kill the player and restart the level.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "KillVolume")
        {
           
            gameManager.RestartGame();



            // reset the values of the UI element timers and the icons.
            spikeUI.GetComponent<Timer>().timer = 0;
            doubleUI.GetComponent<Timer>().timer = 0;

            spikeUI.SetActive(false);
            doubleUI.SetActive(false);
            
            //Reset the speed and the milestoneCount.
            speed = storedSpeed;
            speedMilestoneCount = speedMilestoneCountStored;
            distanceMilestone = distanceMilestoneStored;
            deathSound.Play();


        }
    }

}

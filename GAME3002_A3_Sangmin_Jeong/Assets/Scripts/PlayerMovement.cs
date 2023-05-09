using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody myRigidbody;
    Animator myAnimator;
    CapsuleCollider myBodyCollider;
    BoxCollider myFeetCollider;
    AudioSource audioSource;

    public float MIN_SPEED = 2.0f;
    public float MAX_SPEED = 10.0f;
    public float BASE_SPEED = 5.0f;
    public float speed;
    private const float MIN_JUMPSPEED = 300.0f;
    private const float MAX_JUMPSPEED = 400.0f;
    float jumpSpeed;

    [HideInInspector]
    public bool isAlive = true;
    [HideInInspector]
    public bool isGround = false;
    [HideInInspector]
    public bool isOnIce = false;

    PlayerInput PlayerInput;
    PlayerInputAction playerInputActions;

    public GameManager gm;

    // For Debugging
    public GameObject key;
    private Vector3 initialKeyLocation = Vector3.zero;
    public GameObject door;
    private Vector3 initialDoorLocation = Vector3.zero;

    private void Awake()
    {   
        audioSource = GetComponent<AudioSource>();
        PlayerInput = GetComponent<PlayerInput>();
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider>();
        myFeetCollider = GetComponent<BoxCollider>();

        // Key binding
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.ToKey.performed += Tokey;
        playerInputActions.Player.ToDoor.performed += ToDoor;
        playerInputActions.Player.Restart.performed += Restart;
    }

    void Start()
    {
        jumpSpeed = MIN_JUMPSPEED;
        speed = BASE_SPEED;

        //For Debugging to move the player to initial key location
        if(key)
            initialKeyLocation = key.transform.position;
        if(door)
            initialDoorLocation = door.transform.position;
    }

    void FixedUpdate()
    {
        if (!isAlive) {return;}

        Run();

    }

    private void Update()
    {
    }

    //For Debugging to move the player to initial key location
    public void Tokey(InputAction.CallbackContext context) 
    {
        if (!isAlive) {return;}

        transform.position = initialKeyLocation;
    }

    public void ToDoor(InputAction.CallbackContext context) 
    {
        if (!isAlive) {return;}

        initialDoorLocation = initialDoorLocation + new Vector3(2.0f, 0, 0);
        transform.position = initialDoorLocation;
    }

    public void Restart(InputAction.CallbackContext context) 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Triggers for the animation
    private void OnTriggerEnter(Collider other)
    {
        isGround = true;
        myAnimator.SetBool("isJumping", false);
        if(other.gameObject.tag == "Platform")
        {

        }
        else if(other.gameObject.tag == "IcePlatform")
        {
            isOnIce = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        isGround = true;
        myAnimator.SetBool("isJumping", false);
        if(other.gameObject.tag == "Platform")
        {
            
        }

        if(other.gameObject.tag == "BouncyPlatform")
        {
            
            myRigidbody.AddForce(Vector3.up * 50.0f, ForceMode.Impulse);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isGround = false;
        myAnimator.SetBool("isJumping", true);
        myAnimator.SetBool("isRunning", false);
        if(other.gameObject.tag == "Platform")
        {

        }

        if(other.gameObject.tag == "IcePlatform")
        {
            isOnIce = false;
        }
    }

    public void Jump(InputAction.CallbackContext context) 
    {
        if (!isAlive) {return;}
        if(!myRigidbody) {return; }

        if (isGround)
        {
            myAnimator.SetBool("isRunning", false);
            if(context.performed)
            {
                myAnimator.SetBool("isJumping", true);
                // Jump player
                myRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            }
        }
    }

    void Run()
    {
        //Move player
        moveInput = playerInputActions.Player.Movement.ReadValue<Vector2>();
        if(!isOnIce)
        myRigidbody.velocity = new Vector3(moveInput.x * speed, myRigidbody.velocity.y, moveInput.y * speed);
        else
        {
            myRigidbody.AddForce(new Vector3(moveInput.x * 400.0f, 0, moveInput.y * 400.0f), ForceMode.Force);
        }

        //Animation
        bool playerXSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        bool playerZSpeed = Mathf.Abs(myRigidbody.velocity.z) > Mathf.Epsilon;
        
        if(playerXSpeed || playerZSpeed)
        {
            myAnimator.SetBool("isRunning", true);
            myAnimator.SetBool("isJumping", false);

            //Facing to where player is moving
            Vector3 movementDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            transform.LookAt(transform.position + movementDirection);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boundary")
        {
            Die();
        }


    }

    public void Die()
    {
        audioSource.Play();
        myRigidbody.isKinematic = true;
        gm.currentTime = 0.0f;
        isAlive = false;
        gm.gameoverText.gameObject.SetActive(true);
        myAnimator.SetTrigger("Dying");
    }
}

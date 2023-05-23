using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behaviour : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField]
    private float speed;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float movementAcelleration;

    [SerializeField]
    private float movementDeacelleration;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float gravity;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private bool isCrouching;

    [SerializeField]
    private float crouchSpeed;

    [SerializeField]
    private float bounceForce;

    [SerializeField]
    private GameObject spawnPoint;

    [Header("JumpTimer")]
    [SerializeField]
    private float jumpTimeWindow;
    [SerializeField]
    private float currentJumpTime = 0f;
    [SerializeField]
    private bool jumpDone = false;

    [Header("Camera")]
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float mouseSensivity;

    [Header("Animation")]
    [SerializeField]
    private Animator playerAnimator;

    private bool doubleJump = false;

    private bool tripleJump = false;

    private CharacterController controller;

    private Vector3 finalSpeed = Vector3.zero;
    private Vector3 direction = Vector3.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        mouseSensivity = mainCamera.GetComponent<CameraPlayer>().mouseSensivity;
    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        playerAnimator.SetBool("Grounded", controller.isGrounded);
        MoveCharacter();
        RotateCharacter();
        JumpCharacter();
        if (Input_manager._INPUT_MANAGER.GetCrouchButtonHold())
        {
            Crouch();
        }
        else
        {
            //controller.height = 2f;
            isCrouching = false;
        }
    }
    private void MoveCharacter()
    {
        //Read Input Manager Values Direction
        Vector2 directionInput = Input_manager._INPUT_MANAGER.GetLeftAxisValue();
        if (directionInput != null)
        {
            direction = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f) * new Vector3(directionInput.x, 0f, directionInput.y);
        }

        direction.Normalize();

        if(Input_manager._INPUT_MANAGER.GetLeftAxisButonPressed())
        {
            speed = speed + movementAcelleration * Time.deltaTime;
        }
        else
        {
            speed = speed - movementDeacelleration * Time.deltaTime;
            if (speed < 0f)
            {
                speed = 0f;
            }
        }
        //if (isCrouching)
        //{
        //    maxSpeed = crouchSpeed;
        //}
        //else
        //{
        //    maxSpeed = 6f;
        //}
        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        } 
        else if(isCrouching)
        {
            speed = crouchSpeed;
        }

        finalSpeed.x = direction.x * speed;
        finalSpeed.z = direction.z * speed;

        controller.Move(finalSpeed * Time.deltaTime);

        //Animation
        playerAnimator.SetFloat("Speed", speed);
    }

    private void RotateCharacter()
    {
        if(finalSpeed.magnitude != 0)
        {
            Vector3 rotateInput = Input_manager._INPUT_MANAGER.GetRightAxisValue();
            transform.Rotate(Vector3.up * rotateInput.x * mouseSensivity * Time.deltaTime);

            Quaternion cameraRotation = mainCamera.transform.rotation;
            cameraRotation.x = 0f;
            cameraRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, cameraRotation, 0.1f);
        }
    }

    private void CheckJumpWindow()
    {
        if (currentJumpTime >= jumpTimeWindow || controller.isGrounded)
        {
            jumpDone = true;
            playerAnimator.SetTrigger("WindowEnd");
        }
    }
    private void JumpCharacter()
    {
        //Apply gravity
        direction.y = -1f;

        
        currentJumpTime += Time.deltaTime;
       
        if (controller.isGrounded)
        {
            if (Input_manager._INPUT_MANAGER.GetJumpButtonPressed())
            {
                jumpDone = false;
                tripleJump = false;
                currentJumpTime = 0f;
                Debug.Log("Single");
                playerAnimator.SetBool("Grounded", controller.isGrounded);
                playerAnimator.SetTrigger("SingleJump");
                finalSpeed.y += jumpSpeed;
                doubleJump = true;
            }
            else
            {
                finalSpeed.y = direction.y * gravity * Time.deltaTime;
                jumpDone = true;
                CheckJumpWindow();
            }
        }
        else
        {
            finalSpeed.y += direction.y * gravity * Time.deltaTime;
            CheckJumpWindow();
            if (Input_manager._INPUT_MANAGER.GetJumpButtonPressed() && tripleJump && !jumpDone)
            {
                Debug.Log("Triple");
                playerAnimator.SetTrigger("TripleJump");
                finalSpeed.y += jumpSpeed;
                doubleJump = false;
                tripleJump = false;
            }
            if (Input_manager._INPUT_MANAGER.GetJumpButtonPressed() && doubleJump && !jumpDone)
            {
                Debug.Log("Doble");
                playerAnimator.SetTrigger("DoubleJump");
                finalSpeed.y += jumpSpeed;
                tripleJump = true;
                doubleJump = false;
            }
        }
    }

    private void Crouch()
    {
        isCrouching = true;
        //Cambiar tamaño de z y el tamaño del collider
        //controller.height = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Cambiar de rigidbody a con character controller
        Debug.Log("Trigger");
        if (other.gameObject.tag == "JumpPlatform")
        {
            //playerRigidbody.AddForce(playerRigidbody.transform.up * bounceForce);
        }

        if (other.gameObject.tag == "Death")
        {
            Debug.Log("DeathTouch");
            
            //transform.position = spawnPoint.transform.position;
            controller.transform.position = spawnPoint.transform.position;
            GameManager._GAME_MANAGER.ResetPoints();
        }
    }
}

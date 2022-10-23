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
    private float colliderHeight;

    [SerializeField]
    private float crouchSpeed;

    [Header("Camera")]
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private GameObject cameraTarget;

    [SerializeField]
    private float mouseSensivity;

    private bool doubleJump = false;

    private bool tripleJump = false;

    private CharacterController controller;
    private Rigidbody playerRigidbody;
    private CapsuleCollider capsuleCollider;

    private Vector3 finalSpeed = Vector3.zero;
    private Vector3 direction = Vector3.zero;

    private Vector3 finalRotation = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerRigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        colliderHeight = capsuleCollider.height;
    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        MoveCharacter();
        RotateCharacter();
        JumpCharacter();
        if (Input_manager._INPUT_MANAGER.GetCrouchButtonHold())
        {
            Crouch();
        }
        else
        {
            capsuleCollider.height = colliderHeight;
            controller.height = 2f;
            isCrouching = false;
        }
    }
    private void MoveCharacter()
    {
        //Read Input Manager Values Direction
        Vector2 directionInput = Input_manager._INPUT_MANAGER.GetLeftAxisValue();
        if (directionInput != null)
        {
            direction = directionInput.y * mainCamera.transform.forward + directionInput.x * mainCamera.transform.right;
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
    }

    private void RotateCharacter()
    {
        float rotationX = cameraTarget.transform.rotation.x;
        float rotationY = cameraTarget.transform.rotation.y;
        Vector3 rotateInput = Input_manager._INPUT_MANAGER.GetRightAxisValue();

        rotationX += rotateInput.y * mouseSensivity;
        rotationY += rotateInput.x * mouseSensivity;

        //rotationX = Mathf.Clamp(rotationX, -50f, 50f);

        rotation = new Vector3(rotationX, rotationY, 0);

        cameraTarget.transform.rotation = Quaternion.Euler(rotation);
    }

    private void JumpCharacter()
    {
        //Apply gravity
        direction.y = -1f;

        if (controller.isGrounded)
        {
            if (Input_manager._INPUT_MANAGER.GetJumpButtonPressed())
            {
                Debug.Log("Single");
                finalSpeed.y += jumpSpeed;
                doubleJump = true;
            }
            else
            {
                finalSpeed.y = direction.y * gravity * Time.deltaTime;
            }
        }
        else
        {
            finalSpeed.y += direction.y * gravity * Time.deltaTime;
            if (Input_manager._INPUT_MANAGER.GetJumpButtonPressed() && doubleJump)
            {
                Debug.Log("Doble");
                finalSpeed.y += jumpSpeed;
                tripleJump = true;
                doubleJump = false;
            }
            //if (Input_manager._INPUT_MANAGER.GetJumpButtonPressed() && tripleJump)
            //{
            //    Debug.Log("Triple");
            //    finalSpeed.y += jumpSpeed;
            //    doubleJump = false;
            //    tripleJump = true;
            //}
        }
    }

    private void Crouch()
    {
        isCrouching = true;
        //Cambiar tamaño de z y el tamaño del collider
        capsuleCollider.height = 1;
        controller.height = 1;
    }
}

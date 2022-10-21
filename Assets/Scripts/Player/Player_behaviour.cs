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

    [Header("Camera")]
    [SerializeField]
    private Camera mainCamera;

    private bool doubleJump = false;

    private bool tripleJump = false;

    private CharacterController controller;
    private Rigidbody playerRigidbody;

    private Vector3 finalSpeed = Vector3.zero;
    private Vector3 direction = Vector3.zero;

    private Vector3 finalRotation = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    float rotationX = 0f;
    float rotationY = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        RotateCharacter();
        JumpCharacter();
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
        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }

        finalSpeed.x = direction.x * speed;
        finalSpeed.z = direction.z * speed;

        controller.Move(finalSpeed * Time.deltaTime);
    }

    private void RotateCharacter()
    {
        Vector3 rotateInput = Input_manager._INPUT_MANAGER.GetRightAxisValue();
        if(rotateInput != null)
        {
            rotationX = rotateInput.y;
            rotationY = rotateInput.x;
        }

        rotation = new Vector3(rotationX, rotationY, rotateInput.z) * rotateSpeed;

        transform.Rotate(rotation);
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
}

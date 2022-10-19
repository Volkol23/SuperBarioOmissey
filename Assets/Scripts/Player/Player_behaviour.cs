using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behaviour : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float jumpSpeed = 8f;

    [SerializeField]
    private float gravity = 20f;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private Camera mainCamera;

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

        finalSpeed.x = direction.x * speed;
        finalSpeed.z = direction.z * speed;

        //Apply gravity
        direction.y = -1f;

        if (controller.isGrounded)
        {
            if (Input_manager._INPUT_MANAGER.GetJumpButtonPressed())
            {
                finalSpeed.y = jumpSpeed;
            }
            else
            {
                finalSpeed.y = direction.y * gravity * Time.deltaTime;
            }
        }
        else
        {
            finalSpeed.y += direction.y * gravity * Time.deltaTime;
        }

        controller.Move(finalSpeed * Time.deltaTime);
    }

    private void RotateCharacter()
    {
        Vector2 rotateInput = Input_manager._INPUT_MANAGER.GetRightAxisValue();
        if(rotateInput != null)
        {
            rotationX = rotateInput.y;
            rotationY = rotateInput.x;
        }
        
        rotationX = Mathf.Clamp(rotationX, -50f, 50f);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
        //transform.position
    }
}

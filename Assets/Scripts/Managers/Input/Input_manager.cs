using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_manager : MonoBehaviour
{
    public static Input_manager _INPUT_MANAGER;

    private PlayerInputActions playerInputs;

    private float timeSinceJumpPressed = 0f;
    private Vector2 leftAxisValue = Vector2.zero;
    private Vector3 rightAxisValue = Vector3.zero;
    [SerializeField]
    bool crouchButtonIsBeingHold;

    private void Awake()
    {
        if(_INPUT_MANAGER != null && _INPUT_MANAGER!= this)
        {
            Destroy(_INPUT_MANAGER);
        }
        else
        {
            playerInputs = new PlayerInputActions();
            playerInputs.Character.Enable();

            playerInputs.Character.Jump.performed += JumpButtonPressed;
            playerInputs.Character.Move.performed += LeftAxisValue;
            playerInputs.Character.CameraRotate.performed += RightAxisValue;

            playerInputs.Character.Crouch.performed += CrouchButtonIsBeingHold;
            playerInputs.Character.Crouch.canceled += CrouchButtonIsNotBeingHold;

            _INPUT_MANAGER = this;
            DontDestroyOnLoad(_INPUT_MANAGER);
        }
    }

    private void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;
        InputSystem.Update();
    }

    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("JumpButtonPressed");
        timeSinceJumpPressed = 0f;
    }

    private void LeftAxisValue(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();
        //Debug.Log("Magnitude: " + leftAxisValue.magnitude);
        //Debug.Log("Normalize: " + leftAxisValue.normalized);
    }

    private void RightAxisValue(InputAction.CallbackContext context)
    {
        rightAxisValue = context.ReadValue<Vector2>();
    }

    private void CrouchButtonIsBeingHold(InputAction.CallbackContext context)
    {
        crouchButtonIsBeingHold = true;
    }

    private void CrouchButtonIsNotBeingHold(InputAction.CallbackContext context)
    {
        crouchButtonIsBeingHold = false;
    }

    public bool GetLeftAxisButonPressed()
    {
        return playerInputs.Character.Move.IsPressed();
    }
    public bool GetJumpButtonPressed()
    {
        return timeSinceJumpPressed == 0f;
    }

    public Vector2 GetLeftAxisValue()
    {
        return leftAxisValue;
    }

    public Vector3 GetRightAxisValue()
    {
        return rightAxisValue;
    }

    public bool GetCrouchButtonHold()
    {
        return crouchButtonIsBeingHold;
    }
}

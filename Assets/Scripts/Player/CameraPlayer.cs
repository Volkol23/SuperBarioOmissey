using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform cameraLookAt;

    [SerializeField]
    private float XMinRotation;

    [SerializeField]
    private float XMaxRotation;

    [SerializeField]
    private float distanceToTarget;

    [SerializeField]
    public float mouseSensivity;

    [SerializeField]
    private float rotationX = 0f;

    [SerializeField]
    private float rotationY = 0f;

    private void Update()
    {
        Vector3 rotateInput = Input_manager._INPUT_MANAGER.GetRightAxisValue();
        if (rotateInput.magnitude != 0)
        {
            rotationX += rotateInput.y * mouseSensivity * Time.deltaTime;
            rotationY += rotateInput.x * mouseSensivity * Time.deltaTime;
        }
        rotationX = Mathf.Clamp(rotationX, XMinRotation, XMaxRotation);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
        transform.position = cameraLookAt.transform.position - transform.forward * distanceToTarget;
    }
}

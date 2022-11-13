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

    [SerializeField]
    private float cameraLerp = 12f;

    private void LateUpdate()
    {
        CameraMovememnt();

    }

    void CameraMovememnt()
    {
        Vector3 rotateInput = Input_manager._INPUT_MANAGER.GetRightAxisValue();
        if (rotateInput.magnitude != 0)
        {
            rotationX += rotateInput.y * mouseSensivity;
            rotationY += rotateInput.x * mouseSensivity;
        }
        rotationX = Mathf.Clamp(rotationX, XMinRotation, XMaxRotation);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
        Vector3 finalPosition = Vector3.Lerp(transform.position, cameraLookAt.transform.position - transform.forward * distanceToTarget, cameraLerp * Time.deltaTime);

        RaycastHit hit;
        if(Physics.Linecast(cameraLookAt.transform.position, finalPosition, out hit))
        {
            finalPosition = hit.point;
        }

        transform.position = finalPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    private Rigidbody jumpRigidbody;

    private void Start()
    {
        jumpRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("TriggerPlayer");
            jumpRigidbody.AddForce(jumpRigidbody.transform.up * 500f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField]
    private float bounceForce;

    private Rigidbody jumpPlatformRigidbody;
    private void Awake()
    {
        jumpPlatformRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

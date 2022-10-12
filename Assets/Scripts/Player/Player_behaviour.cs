using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behaviour : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jump_speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    currentSpeed += acceleration * Time.deltaTime;
        //}
        //else
        //{
        //    currentSpeed -= acceleration * Time.deltaTime;
        //}

        //currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        //transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }

    public float GetCurrentSpeed()
    {
        return this.speed;
    }
}

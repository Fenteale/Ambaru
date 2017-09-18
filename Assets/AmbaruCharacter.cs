﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbaruCharacter : MonoBehaviour
{
    public float speed;
    public float turnSpeed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);
        //rb.transform.eulerAngles = new Vector3(Mathf.Clamp(rb.rotation.x, 0.0f, 0.0f), rb.rotation.y, rb.rotation.z);
        transform.Rotate(0, moveHorizontal * turnSpeed * rb.velocity.magnitude/speed, 0);
        //transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, 0.0f, 0.0f), transform.eulerAngles.y, transform.eulerAngles.z);
        rb.AddRelativeForce(movement * speed );

        
    }

}
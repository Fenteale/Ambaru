using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbaruCharacter : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float turnSpeed;
    public float gravity = 1.0f;

    private Rigidbody rb;
    private bool eGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 oldpos;

        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);

        oldpos = transform.position;
        //rb.transform.eulerAngles = new Vector3(Mathf.Clamp(rb.rotation.x, 0.0f, 0.0f), rb.rotation.y, rb.rotation.z);
	transform.Rotate(0, moveHorizontal * turnSpeed * moveVertical, 0);
	
        //transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, 0.0f, 0.0f), transform.eulerAngles.y, transform.eulerAngles.z);
        transform.Translate(Vector3.forward * moveVertical * speed);

        if (eGravity)
        {
            RaycastHit hit;
            Ray downRay = new Ray(transform.position, -Vector3.up);
            if (Physics.Raycast(downRay, out hit))
            {
                float hoverError = hit.distance - gravity;
                if (hoverError > 0)
                {
                    transform.Translate(Vector3.down * gravity);
                    //rb.AddForce(lift * Vector3.up);
                }
                else
                    transform.Translate(Vector3.down * hit.distance);
            }
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        eGravity = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        eGravity = true;
    }
}

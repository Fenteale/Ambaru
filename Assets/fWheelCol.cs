using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fWheelCol : MonoBehaviour {

    // Use this for initialization
    public float gravity = 0.01f;
    public GameObject main;

    private bool eGravity = true;


	void Start () {
        Physics.IgnoreCollision(main.GetComponent<Collider>(), GetComponent<Collider>());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fwheel_rotation : MonoBehaviour {


    public float rotationSpeed = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        transform.Rotate(moveVertical*rotationSpeed, 0, 0);
    }
}

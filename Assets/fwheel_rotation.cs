using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fwheel_rotation : MonoBehaviour {


    public float rotationSpeed = 2;

    public GameObject c;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //transform.SetPositionAndRotation(c.transform.position, )
        transform.position = c.transform.position;
        transform.Rotate(moveVertical*rotationSpeed, 0, 0);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, c.transform.rotation.eulerAngles.y, c.transform.rotation.eulerAngles.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbaruCharacterTest : MonoBehaviour {
    public float speed;
    public float maxSpeed = 100;
    public float turnSpeed;
    public float gravity = 1.0f;
    public float friction = 0.1f;
    public float brakeStrength = 2.0f;
    public float acc = 1f;
    [Tooltip("How many units above the root of the car to start checking for collision.")]
    public float collisionOffset = 1f;

    public GameObject FrontWheel;
    public GameObject BackWheel;

    private GameObject fwpos;
    private GameObject bwpos;
    private GameObject mod;
    private float modyoffset;
    private float modzoffset;
    private float bwrad;
    private float wheeldistance;

    private float fgrav = 0.0f;
    private float bgrav = 0.0f;

    private float originfWheel;

    private float maxGravity;

    private bool eGravity;

    private Vector3 fwcolold;

    // Use this for initialization
    void Start () {
        fwpos = this.gameObject.transform.GetChild(1).gameObject;
        bwpos = this.gameObject.transform.GetChild(2).gameObject;
        mod = this.gameObject.transform.GetChild(0).gameObject;
        modyoffset = mod.transform.localPosition.y;
        modzoffset = mod.transform.localPosition.z;

        FrontWheel.transform.SetPositionAndRotation(new Vector3(fwpos.gameObject.transform.position.x, fwpos.transform.position.y, fwpos.gameObject.transform.position.z), Quaternion.Euler(fwpos.transform.rotation.eulerAngles.x, FrontWheel.transform.rotation.eulerAngles.y, FrontWheel.transform.rotation.eulerAngles.z));
        BackWheel.transform.SetPositionAndRotation(new Vector3(bwpos.gameObject.transform.position.x, bwpos.transform.position.y, bwpos.gameObject.transform.position.z), Quaternion.Euler(bwpos.transform.rotation.eulerAngles.x, BackWheel.transform.rotation.eulerAngles.y, BackWheel.transform.rotation.eulerAngles.z));
    }
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (Input.GetKey("w"))
        {
            speed += acc;
        }
        else if (Input.GetKey("s"))
        {
            speed += -acc;
        }
        else if (Input.GetKey("left shift"))
        {
            if (speed > brakeStrength)
            {
                speed -= brakeStrength;
            }
            else if (speed < -brakeStrength)
            {
                speed += brakeStrength;
            }
            else
            {
                speed = 0;
            }
        }
        else
        {
            if (speed > friction)
            {
                speed -= friction;
            }
            else if (speed < -friction)
            {
                speed += friction;
            }
            else
            {
                speed = 0;
            }
        }
        if (System.Math.Abs(speed) > maxSpeed)
        {
            speed = (System.Math.Abs(speed) / speed) * maxSpeed;
        }
        //float moveVertical = Input.GetAxis("Vertical");
        transform.position = transform.position+(-mod.transform.up * speed * Time.deltaTime);
        //MOVEMENT CODE
        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);
        if (speed != 0)
        {
            transform.Rotate(0, moveHorizontal * (speed / maxSpeed) * turnSpeed * Time.deltaTime, 0);
        }
        





        float pitchAngle = Mathf.Rad2Deg * Mathf.Atan2(FrontWheel.transform.localPosition.z - BackWheel.transform.localPosition.z, FrontWheel.transform.localPosition.y - BackWheel.transform.localPosition.y);

        

        this.gameObject.transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(180.0f - pitchAngle, transform.eulerAngles.y + 180.0f, transform.eulerAngles.z);
        mod.transform.position = BackWheel.transform.position;
        mod.transform.localPosition = new Vector3(mod.transform.localPosition.x, mod.transform.localPosition.y + modyoffset + ((FrontWheel.transform.localPosition.y - BackWheel.transform.localPosition.y) / 2.0f), ((FrontWheel.transform.localPosition.z - BackWheel.transform.localPosition.z) / 2.0f));
        //mod.transform.position = BackWheel.transform.position;
    }
}

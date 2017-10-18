using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbaruCharacter : MonoBehaviour
{
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

    public float fgrav = 0.0f;
    public float bgrav = 0.0f;

    private float maxGravity;

    private bool eGravity;

    void Start()
    {

        fwpos = this.gameObject.transform.GetChild(1).gameObject;
        bwpos = this.gameObject.transform.GetChild(2).gameObject;
        mod = this.gameObject.transform.GetChild(0).gameObject;
        modyoffset = mod.transform.localPosition.y;
        modzoffset = mod.transform.localPosition.z;


        fgrav = gravity;
        bgrav = gravity;
        maxGravity = gravity*10;

        FrontWheel.transform.SetPositionAndRotation(new Vector3(fwpos.gameObject.transform.position.x, fwpos.transform.position.y, fwpos.gameObject.transform.position.z), Quaternion.Euler(fwpos.transform.rotation.eulerAngles.x, FrontWheel.transform.rotation.eulerAngles.y, FrontWheel.transform.rotation.eulerAngles.z));
        BackWheel.transform.SetPositionAndRotation(new Vector3(bwpos.gameObject.transform.position.x, bwpos.transform.position.y, bwpos.gameObject.transform.position.z), Quaternion.Euler(bwpos.transform.rotation.eulerAngles.x, BackWheel.transform.rotation.eulerAngles.y, BackWheel.transform.rotation.eulerAngles.z));
        bwrad = BackWheel.GetComponent<CapsuleCollider>().radius;
        wheeldistance = fwpos.transform.localPosition.z;
    }
   

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
	float moveVertical = Input.GetAxis("Vertical");
	if(Input.GetKey("w")){
		speed += acc;
	}
	else if(Input.GetKey("s")){
		speed += -acc;
	}
	else if(Input.GetKey("left shift")){
		if(speed > brakeStrength){
			speed -= brakeStrength;
		}
		else if(speed < -brakeStrength){
			speed += brakeStrength;
		}
		else{
			speed = 0;
		}
	}
	else{
		if(speed > friction){
			speed -= friction;
		}
		else if(speed < -friction){
			speed += friction;
		}
		else{
			speed = 0;
		}
	}
	if(System.Math.Abs(speed) > maxSpeed){
		speed = (System.Math.Abs(speed)/speed) * maxSpeed;
	}
        //float moveVertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //MOVEMENT CODE
        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);
        if(speed != 0){
		transform.Rotate(0, moveHorizontal * (speed/maxSpeed) * turnSpeed *Time.deltaTime, 0);
	}
        //END MOVEMENT CODE

        //SET WHEELS ON GROUND
        float oldfwheel = FrontWheel.transform.position.y;
        float fdelta = 0.0f;
        float bdelta = 0.0f;

        RaycastHit hit;
        Ray downRay = new Ray(transform.position + Vector3.up* collisionOffset, -Vector3.up);
        if(Physics.Raycast(downRay, out hit))  //BACK WHEEL
        {
            if(-bgrav < -hit.distance + bwrad + collisionOffset)
            {
                bgrav = 0.0f;
                bdelta = -hit.distance + bwrad + collisionOffset;
            }
            else
            {
                bgrav += gravity*Time.deltaTime;
                if (bgrav > maxGravity)
                    bgrav = maxGravity;
                bdelta = (-bgrav) * Time.deltaTime;
            }
            
        }

        downRay = new Ray(fwpos.transform.position+Vector3.up* collisionOffset, -Vector3.up);
        if (Physics.Raycast(downRay, out hit)) //FRONT WHEEL
        {
            if (fgrav + bwrad + collisionOffset > hit.distance)
            {
                fgrav = 0.0f;

                fdelta = -hit.distance + bwrad + collisionOffset;
            }
            else
            {
                fgrav += gravity * Time.deltaTime;
                if (fgrav > maxGravity)
                    fgrav = maxGravity;

                fdelta = -fgrav * Time.deltaTime;
                //FrontWheel.transform.position = new Vector3(FrontWheel.transform.position.x, FrontWheel.transform.position.y + ((-bgrav) * Time.deltaTime), FrontWheel.transform.position.z);
            }
        }

        transform.position = transform.position + (Vector3.up * bdelta);
        FrontWheel.transform.position = new Vector3(fwpos.transform.position.x, oldfwheel + bdelta, fwpos.transform.position.z);

        //END SET WHEELS ON GROUND


        //SET BODY ROTATION
        float pitchAngle = Mathf.Rad2Deg * Mathf.Atan2(FrontWheel.transform.localPosition.z - BackWheel.transform.localPosition.z, FrontWheel.transform.localPosition.y - BackWheel.transform.localPosition.y);

        this.gameObject.transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(180.0f-pitchAngle, transform.eulerAngles.y+180.0f, transform.eulerAngles.z);

        fwpos.transform.localPosition = new Vector3(fwpos.transform.localPosition.x, fwpos.transform.localPosition.y, Mathf.Sqrt(Mathf.Pow(wheeldistance, 2) - Mathf.Pow(FrontWheel.transform.localPosition.y - BackWheel.transform.localPosition.y, 2)));
        //END SET BODY ROTATION

        //Stuff previously in "late update" function
        //FrontWheel.transform.localPosition = new Vector3(fwpos.transform.localPosition.x, FrontWheel.transform.localPosition.y, fwpos.transform.localPosition.z);
        mod.transform.localPosition = new Vector3(mod.transform.localPosition.x, modyoffset + ((FrontWheel.transform.localPosition.y - BackWheel.transform.localPosition.y) / 2.0f), ((FrontWheel.transform.localPosition.z - BackWheel.transform.localPosition.z) / 2.0f));




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

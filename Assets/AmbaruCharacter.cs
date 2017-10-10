using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbaruCharacter : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float turnSpeed;
    public float gravity = 1.0f;

    public GameObject FrontWheel;
    public GameObject BackWheel;

    private GameObject fwpos;
    private GameObject bwpos;
    private GameObject mod;
    private float modoffset;
    private float bwrad;
    private float wheeldistance;
    private bool eGravity;

    void Start()
    {

        fwpos = this.gameObject.transform.GetChild(1).gameObject;
        bwpos = this.gameObject.transform.GetChild(2).gameObject;
        mod = this.gameObject.transform.GetChild(0).gameObject;
        modoffset = mod.transform.localPosition.y;

        //Physics.IgnoreCollision(FrontWheel.GetComponent<Collider>(), GetComponent<Collider>());
        //Physics.IgnoreCollision(BackWheel.GetComponent<Collider>(), GetComponent<Collider>());

        FrontWheel.transform.SetPositionAndRotation(new Vector3(fwpos.gameObject.transform.position.x, fwpos.transform.position.y, fwpos.gameObject.transform.position.z), Quaternion.Euler(fwpos.transform.rotation.eulerAngles.x, FrontWheel.transform.rotation.eulerAngles.y, FrontWheel.transform.rotation.eulerAngles.z));
        BackWheel.transform.SetPositionAndRotation(new Vector3(bwpos.gameObject.transform.position.x, bwpos.transform.position.y, bwpos.gameObject.transform.position.z), Quaternion.Euler(bwpos.transform.rotation.eulerAngles.x, BackWheel.transform.rotation.eulerAngles.y, BackWheel.transform.rotation.eulerAngles.z));
        bwrad = BackWheel.GetComponent<CapsuleCollider>().radius;
        wheeldistance = fwpos.transform.localPosition.z;
    }

    private void LateUpdate()
    {
        FrontWheel.transform.localPosition = new Vector3(fwpos.transform.localPosition.x, FrontWheel.transform.localPosition.y, fwpos.transform.localPosition.z);
        mod.transform.localPosition = new Vector3(mod.transform.localPosition.x, modoffset+((FrontWheel.transform.localPosition.y - BackWheel.transform.localPosition.y) / 2.0f), mod.transform.localPosition.z);
        //FrontWheel.transform.SetPositionAndRotation(new Vector3(fwpos.gameObject.transform.position.x, FrontWheel.transform.position.y, fwpos.gameObject.transform.position.z), Quaternion.Euler(fwpos.transform.rotation.eulerAngles.x, FrontWheel.transform.rotation.eulerAngles.y, FrontWheel.transform.rotation.eulerAngles.z));
        //BackWheel.transform.SetPositionAndRotation(new Vector3(bwpos.gameObject.transform.position.x, BackWheel.transform.position.y, bwpos.gameObject.transform.position.z), Quaternion.Euler(bwpos.transform.rotation.eulerAngles.x, BackWheel.transform.rotation.eulerAngles.y, BackWheel.transform.rotation.eulerAngles.z));
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //MOVEMENT CODE
        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);
        transform.Rotate(0, moveHorizontal * turnSpeed * moveVertical, 0);
        //END MOVEMENT CODE

        //SET WHEELS ON GROUND
        RaycastHit hit;
        Ray downRay = new Ray(transform.position + Vector3.up, -Vector3.up);
        if(Physics.Raycast(downRay, out hit))
        {
            transform.position = transform.position + (Vector3.up *( -hit.distance + bwrad + 1f));
            //BackWheel.transform.localPosition = new Vector3(BackWheel.transform.localPosition.x, -hit.distance+bwrad+1f, BackWheel.transform.localPosition.z);
        }

        downRay = new Ray(fwpos.transform.position+Vector3.up, -Vector3.up);
        if (Physics.Raycast(downRay, out hit))
        {
            FrontWheel.transform.localPosition = new Vector3(FrontWheel.transform.localPosition.x, -hit.distance + bwrad+1f, FrontWheel.transform.localPosition.z);
        }

        //END SET WHEELS ON GROUND


        //SET BODY ROTATION
        float pitchAngle = Mathf.Rad2Deg * Mathf.Atan2(FrontWheel.transform.localPosition.z - BackWheel.transform.localPosition.z, FrontWheel.transform.localPosition.y - BackWheel.transform.localPosition.y);

        this.gameObject.transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(180.0f-pitchAngle, transform.eulerAngles.y+180.0f, transform.eulerAngles.z);

        fwpos.transform.localPosition = new Vector3(fwpos.transform.localPosition.x, fwpos.transform.localPosition.y, Mathf.Sqrt(Mathf.Pow(wheeldistance, 2) - Mathf.Pow(FrontWheel.transform.localPosition.y - BackWheel.transform.localPosition.y, 2)));
        //END SET BODY ROTATION
        //transform.position = new Vector3(transform.position.x, BackWheel.transform.position.y, transform.position.z);
        transform.Translate(Vector3.forward * moveVertical * speed);

        
        

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

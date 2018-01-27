using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbaruCharacter : MonoBehaviour
{
    public float speed = 10.0f;

    public float gravity = 40.0f;

    public float damping = 0.0f;

    [Tooltip("How many units above the root of the car to start checking for collision.")]
    public float collisionOffset = 1f;

    [Tooltip("The K factor used in the spring equation.")]
    public float kFactor = 1.0f;

    public float wheelMass = 1.0f;

    public float bodyMass = 1.0f;

    public GameObject FrontWheel;
    public GameObject FrontWheelWell;
    public GameObject BackWheel;


    public LayerMask mask = -1;


    private Vector3 fwheelpos;

    private float grav = 0.0f;
    private float omegaprime;

    private float rotvel = 0.0f;

    private float oldtimeval1 = 0.0f;
    private float oldtimeval2 = 0.0f;

    private float fWheelXm = 0.0f;
    private float oldhitdist = 0.0f;


    void Start()
    {
        if (Mathf.Pow(damping / (2 * wheelMass), 2) > (kFactor / wheelMass))
            damping = Mathf.Sqrt(kFactor / wheelMass) * 2 * wheelMass;
        omegaprime = Mathf.Sqrt((kFactor/wheelMass)-Mathf.Pow(damping/(2*wheelMass), 2));

        fwheelpos = FrontWheel.transform.localPosition;
    }
   

    void Update()
    {
        //CONTROLS/MOVEMENT
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        
        //END MOVEMENT CODE

        //SET WHEELS ON GROUND

        float bdelta = 0.0f;
        


        RaycastHit hit;
        Ray downRay = new Ray(transform.position + fwheelpos + Vector3.up * collisionOffset, -Vector3.up);
        if (Physics.Raycast(downRay, out hit, 1000.0f, mask))
        {
            FrontWheel.transform.localPosition = new Vector3(fwheelpos.x, -hit.distance + collisionOffset, fwheelpos.z);
            //fWheelXm = +hit.distance - collisionOffset;
        }

        if (Mathf.Round(hit.distance*100.0f) / 100.0f != Mathf.Round(oldhitdist* 100.0f) / 100.0f)
        {
            Debug.Log("Should Reset, hit: "+hit.distance+" oldhit: "+oldhitdist);
            fWheelXm = FrontWheelWell.transform.localPosition.y - hit.distance; //gotta fix this later, idk what the deal is.  closest is "hit.distance - oldhitdist" but it should update from where the wheel well actually is... 
            ResetOsc();
        }
        oldhitdist = hit.distance;
            /*
            RaycastHit hit;
            Ray downRay = new Ray(FrontWheel.transform.position + Vector3.up* collisionOffset, -Vector3.up);

            if (Physics.Raycast(downRay, out hit, 1000.0f, mask))
            {
                if(grav * Time.deltaTime> hit.distance - collisionOffset)
                {
                    grav = 0.0f;
                    bdelta = -hit.distance + collisionOffset;
                }
                else
                {
                    grav += gravity*Time.deltaTime;
                    bdelta = (-grav) * Time.deltaTime;
                }

            }

            */

            //transform.position = transform.position + (Vector3.up * bdelta);


        OscillateBody(fWheelXm);

        //transform.tran
        //transform.Translate(moveVertical * transform.forward * Time.deltaTime * speed);
        transform.Translate(moveVertical * new Vector3(0.0f,0.0f,1.0f) * Time.deltaTime * speed);

    }
    
    void OscillateBody(float xm)
    {
        FrontWheelWell.transform.localPosition = Vector3.up * xm * Mathf.Exp((-damping * (Time.deltaTime+oldtimeval1)) / (2 * wheelMass)) * Mathf.Cos(omegaprime * Time.deltaTime + oldtimeval2) + FrontWheel.transform.localPosition;
        oldtimeval1 += Time.deltaTime;
        oldtimeval2 = omegaprime * Time.deltaTime + oldtimeval2;
    }

    void ResetOsc()
    {
        oldtimeval1 = 0.0f;
        oldtimeval2 = 0.0f;
    }
    
}

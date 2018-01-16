using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbaruCharacter : MonoBehaviour
{
    public float speed = 10.0f;

    public float gravity = 40.0f;

    [Tooltip("How many units above the root of the car to start checking for collision.")]
    public float collisionOffset = 1f;

    [Tooltip("The K/M Ratio used in the rotational acceleration formula (lower it is, slower the car will rotate).")]
    public float kmRatio = 1.0f;

    public GameObject FrontWheel;
    public GameObject BackWheel;

    public LayerMask mask = -1;

    private float grav = 0.0f;

    private float rotvel = 0.0f;


    void Start()
    {

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

        

        transform.position = transform.position + (Vector3.up * bdelta);

      


        //transform.tran
        //transform.Translate(moveVertical * transform.forward * Time.deltaTime * speed);
        transform.Translate(moveVertical * new Vector3(0.0f,0.0f,1.0f) * Time.deltaTime * speed);

    }
    
    

    
}

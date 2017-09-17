using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonS : MonoBehaviour {

    public Transform lookAt;
    public Transform camTransform;

    public float yDistance = 1.0f;

    private Camera cam;

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 80.0f;

    public float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensitivityX = 4.0f;
    private float sensitivityY = 1.0f;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;

    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Vector3 offsety = new Vector3(0, yDistance, 0);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + offsety + rotation * dir;

        camTransform.LookAt(lookAt.position + offsety);
    }
}

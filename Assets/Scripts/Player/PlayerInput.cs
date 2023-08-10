using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [SerializeField] private float lookSensitivity = 3f;

    [SerializeField] private float thrusterForce = 20f;
    
    [SerializeField] private PlayerController controller;

    [SerializeField] private ConfigurableJoint joint;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var xMov = Input.GetAxisRaw("Horizontal");
        var yMov = Input.GetAxisRaw("Vertical");

        var velocity = (transform.right * xMov + transform.forward * yMov).normalized * speed;
        controller.Move(velocity);

        var xMouse = Input.GetAxisRaw("Mouse X");
        var yMouse = Input.GetAxisRaw("Mouse Y");

        var yRotation = new Vector3(0f, xMouse, 0f) * lookSensitivity;
        var xRotation = new Vector3(-yMouse, 0f, 0f) * lookSensitivity;
        controller.Rotate(xRotation, yRotation);

        var force = Vector3.zero;

        if (Input.GetButton("Jump"))
        {
            force = Vector3.up * thrusterForce;
            joint.yDrive = new JointDrive
            {
                positionSpring = 0f,
                positionDamper = 0f,
                maximumForce = 0f
            };
        }
        else
        {
            joint.yDrive = new JointDrive
            {
                positionSpring = 20f,
                positionDamper = 0f,
                maximumForce = 40f
            };
        }
        controller.Thrust(force);
    }
}

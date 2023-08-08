using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private PlayerController controller;
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
    }
}

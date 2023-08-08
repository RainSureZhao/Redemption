using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    
    [SerializeField]
    private Camera cam;
    
    private Vector3 _velocity = Vector3.zero; // 速度，每秒钟移动的距离
    
    private Vector3 _yRotation = Vector3.zero; // 旋转角色
    
    private Vector3 _xRotation = Vector3.zero; // 旋转视角
    public void Move(Vector3 velocity)
    {
        _velocity = velocity;
    }

    public void Rotate(Vector3 xRotation, Vector3 yRotation)
    {
        _xRotation = xRotation;
        _yRotation = yRotation;
    }
    private void PerformMovement()
    {
        if (_velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + _velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        if (_yRotation != Vector3.zero)
        {
            rb.transform.Rotate(_yRotation);
        }

        if (_xRotation != Vector3.zero)
        {
            cam.transform.Rotate(_xRotation);
        }
    }
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }
}

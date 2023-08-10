using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    [SerializeField] private Camera cam;
    
    private Vector3 _velocity = Vector3.zero; // 速度，每秒钟移动的距离
    
    private Vector3 _yRotation = Vector3.zero; // 旋转角色的速度
    
    private Vector3 _xRotation = Vector3.zero; // 旋转视角的速度

    private float _cameraRotationTotal = 0f;   // 累计转了多少度

    [SerializeField] private float cameraRotationLimit = 85f;

    private Vector3 _thrusterForce = Vector3.zero; // 向上的推力
    public void Move(Vector3 velocity)
    {
        _velocity = velocity;
    }

    public void Rotate(Vector3 xRotation, Vector3 yRotation)
    {
        _xRotation = xRotation;
        _yRotation = yRotation;
    }

    public void Thrust(Vector3 thrustForce)
    {
        _thrusterForce = thrustForce;
    }
    private void PerformMovement()
    {
        if (_velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + _velocity * Time.fixedDeltaTime);
        }

        if (_thrusterForce != Vector3.zero)
        {
            rb.AddForce(_thrusterForce, ForceMode.Force);
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
            _cameraRotationTotal += _xRotation.x;
            _cameraRotationTotal = Math.Clamp(_cameraRotationTotal, -cameraRotationLimit, cameraRotationLimit);
            cam.transform.localEulerAngles = new Vector3(_cameraRotationTotal, 0f, 0f);
        }
    }
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    [SerializeField] private Camera cam;
    
    private Vector3 _velocity = Vector3.zero; // 速度，每秒钟移动的距离
    
    private Vector3 _yRotation = Vector3.zero; // 旋转角色的速度
    
    private Vector3 _xRotation = Vector3.zero; // 旋转视角的速度

    private float recoilForce = 0f; // 累积的后坐力

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

    public void AddRecoilForce(float newRecoilForce)
    {
        recoilForce += newRecoilForce;
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
        if (recoilForce < 0.1f)
        {
            recoilForce = 0f;
        }

        if (_yRotation != Vector3.zero || recoilForce > 0f)
        {
            Random rd = new Random();
            rb.transform.Rotate(_yRotation + rb.transform.up * (float)(rd.NextDouble() * 2 - 1) * 2 * recoilForce);
        }

        if (_xRotation != Vector3.zero || recoilForce > 0f)
        {
            _cameraRotationTotal += _xRotation.x - recoilForce;
            _cameraRotationTotal = Math.Clamp(_cameraRotationTotal, -cameraRotationLimit, cameraRotationLimit);
            cam.transform.localEulerAngles = new Vector3(_cameraRotationTotal, 0f, 0f);
        }

        recoilForce *= 0.5f;
    }
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }
}

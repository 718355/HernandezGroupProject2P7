using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    private float _speed;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 50.0f;

    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    private GameObject _mainCamera;
    private void Awake()
    {
        if(_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
}

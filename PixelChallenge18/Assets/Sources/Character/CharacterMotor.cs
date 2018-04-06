﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour {

    [Header("Parameters")]
    [SerializeField] private float _moveSpeed = 1f;

    private Rigidbody _rigidbody;
    private CharacterInputs _inputs;

    private Vector2 _velocity;
    private bool _hasInputToProcess;    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputs = GetComponent<CharacterInputs>();
        
        _inputs.LeftJoystickInputEmitted += HandleLeftJoystickInput;
    }

    private void HandleLeftJoystickInput(Vector2 input)
    {
        Move(input * _moveSpeed * Time.deltaTime);
        if(!Mathf.Approximately(input.magnitude, 0f)){
            UpdateOrientation(input);
        }
    }

    public void Move(Vector2 velocity)
    {
        _velocity = velocity;
        _hasInputToProcess = true;
    }

    private void FixedUpdate()
    {
        if (_hasInputToProcess)
        {
            _rigidbody.velocity = new Vector3(_velocity.x, _rigidbody.velocity.y, _velocity.y);
            _hasInputToProcess = false;
        }
    }

    private void UpdateOrientation(Vector2 orientation)
    {
        _rigidbody.transform.rotation = Quaternion.LookRotation(new Vector3(orientation.x, 0f, orientation.y), Vector3.up);
    }
}

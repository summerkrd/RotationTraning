using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _boostMultiplier;

    private Animator _animator;
    private Rigidbody _rb;

    private Vector3 _movement;
    private Vector3 _walkSpeed => _movement * _moveSpeed;
    private Vector3 _boostSpeed => _movement * _moveSpeed * _boostMultiplier;

    private string _isRunning = "isRunning";

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        MoveBody();
        RotateBody();
    }

    private void MoveBody()
    {
        Vector3 targetSpeed = Input.GetKey(KeyCode.LeftShift) ? _boostSpeed : _walkSpeed;
        _rb.velocity = targetSpeed;
        
        _animator.SetBool(_isRunning, _rb.velocity != Vector3.zero);        
    }

    private void RotateBody()
    {
        if (_rb.velocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_rb.velocity, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }           
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        _movement = new Vector3(verticalInput, 0f, -horizontalInput).normalized;
    }



}

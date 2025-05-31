using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    
    private PlayerInputActions _playerControls;
    private InputAction _move;
    private InputAction _jump;
    private InputAction _interact;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float turnSpeed;
    
    [Header("Gravity Settings")]
    [SerializeField] private float gravity;
    [SerializeField] private float fallMultiplier;
    
    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    private Vector3 _moveDirection = Vector3.zero;
    public Vector2 Input2D { get; private set; }
    public bool IsJumping { get; private set; }
    public bool isInteracting;
    
    private void Awake()
    {
        _playerControls = new PlayerInputActions();
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    private void OnEnable()
    {
        _move = _playerControls.Player.Move;
        _move.Enable();
        
        _jump = _playerControls.Player.Jump;
        _jump.Enable();
        _jump.performed += Jump;

        _interact = _playerControls.Player.Interact;
        _interact.Enable();
        _interact.performed += Interact;
    }

    private void OnDisable()
    {
        _move.Disable();
        
        _jump.Disable();
        _jump.performed -= Jump;
        
        _interact.Disable();
        _interact.performed -= Interact;
    }
    

    // Update is called once per frame
    void Update()
    {
        Input2D = _move.ReadValue<Vector2>();
        var input = new Vector3(Input2D.x, 0f, Input2D.y);
        _moveDirection = Quaternion.Euler(0, 45, 0) * input;
    }

    private void FixedUpdate()
    {
        if (IsGrounded()) IsJumping = false;
        
        if (_moveDirection.sqrMagnitude > 0.01f)
        {
            var targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
        }
        
        if (_rb.velocity.y < 0) _rb.AddForce(Vector3.down * (Math.Abs(gravity) * fallMultiplier), ForceMode.Acceleration);
        else _rb.AddForce(Vector3.down * Math.Abs(gravity), ForceMode.Acceleration);
        
        
        var targetVelocity = new Vector3(_moveDirection.x, 0, _moveDirection.z) * moveSpeed;
        var currentVelocity = _rb.velocity;
        var velocityChange = targetVelocity - new Vector3(currentVelocity.x, 0, currentVelocity.z);

        _rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    
    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!IsGrounded()) return;
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        IsJumping = true;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        isInteracting = true;
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Managers;
using TMPro;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rb;

        private PlayerInputActions PlayerControls { get; set; }
        private InputAction _move, _jump, _interact;

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
        
        [Header("References")]
        [SerializeField] private string heldItem;
        [SerializeField] private TextMeshProUGUI heldItemText;
        
        private GameManager _gameManager;
        private InteractableController _currentInteractable;
        
        public bool isInverted;
        public bool isSwapped;
        
        private Vector3 _moveDirection = Vector3.zero;
        public Vector2 Input2D { get; private set; }
        public bool IsJumping { get; private set; }
        public bool isInteracting;
        
        private void Awake()
        {
            PlayerControls = new PlayerInputActions();
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
        }

        private void OnEnable()
        {
            EnableControls("Player");
        }

        private void OnDisable()
        {
            DisableControls();
        }

        public void UpdateControlScheme(bool invertedActive, bool swappedActive)
        {
            ResetControlMap();

            switch (invertedActive)
            {
                case true when swappedActive:
                    EnableControls("PlayerSwapHandsInverted");
                    break;
                case true:
                    EnableControls("PlayerInverted");
                    break;
                default:
                    EnableControls(swappedActive ? "PlayerSwapHands" : "Player");
                    break;
            }
        }

        private void ResetControlMap()
        {
            PlayerControls.Player.Disable();
            PlayerControls.PlayerInverted.Disable();
            PlayerControls.PlayerSwapHands.Disable();
            PlayerControls.PlayerSwapHandsInverted.Disable();
        }

        public void EnableControls(string controlMap)
        {
            DisableControls();
            
            var map = PlayerControls.asset.FindActionMap(controlMap);

            if (map != null)
            {
                map.Enable();

                _move = map.FindAction("Move");
                _jump = map.FindAction("Jump");
                _interact = map.FindAction("Interact");
                
                if (_jump != null) _jump.performed += Jump;
                if (_interact != null) _interact.performed += Interact;
            }
        }

        private void DisableControls()
        {
            foreach (var map in PlayerControls.asset.actionMaps)
                map.Disable();
            
            if (_jump != null) _jump.performed -= Jump;
            if (_interact != null) _interact.performed -= Interact;
        }

        private void Start()
        {
            heldItem = "None";
        }
        
        void Update()
        {
            heldItemText.text = heldItem;
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
            if (_currentInteractable) GameManager.Instance.SetGameText(_currentInteractable.InteractWithPlayer(this));
            
            isInteracting = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InteractableController interactable)) _currentInteractable = interactable;
        }

        private void OnTriggerExit(Collider other)
        {
            if (_currentInteractable && other.gameObject == _currentInteractable.gameObject) _currentInteractable = null;
        }
        
        public void PickUpItem(string itemName)
        {
            heldItem = itemName;
        }

        public bool HasNeededItem(string itemName)
        {
            return heldItem == itemName;
        }
    }
}

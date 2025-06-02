using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public class AnimationStateController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerController playerController;
        private PlayerInputActions _playerControls;
        private InputAction _move;
        private InputAction _jump;

        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int IsFalling = Animator.StringToHash("isFalling");
        private static readonly int Interact = Animator.StringToHash("interact");

        private void Update()
        {
            var moveInput = playerController.Input2D;
            var jumpInput = playerController.IsJumping;
            var interactInput = playerController.isInteracting;

            animator.SetBool(IsRunning, moveInput.sqrMagnitude > 0.01f);
            animator.SetBool(IsJumping, jumpInput);
            animator.SetBool(IsFalling, !playerController.IsGrounded());
            if (interactInput)
            {
                animator.SetTrigger(Interact);
                playerController.isInteracting = false;
            }
        }
    }
}

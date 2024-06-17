using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Movement speed of the player.")]
    public float speed = 3f;

    [Tooltip("Rotation speed of the player.")]
    public float rotationSpeed = 10f;

    [Tooltip("Reference to the main camera.")]
    public Transform cameraTransform;

    private PlayerControls playerInput;
    private InputAction moveAction;

    private Vector2 moveInput;
    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        playerInput = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        moveAction = playerInput.Player.Move;
        moveAction.Enable();
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        bool isRunning = moveInput.magnitude > 0;
        animator.SetBool("isRunning", isRunning);
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 move = forward * moveDirection.z + right * moveDirection.x;
            move *= speed * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + move);

            Quaternion targetRotation = Quaternion.LookRotation(move);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    // To setup dancing and thinking animations.
/*
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("DanceTrigger"))
        {
            animator.SetBool("isDancing", true);
        }
        else if (collision.gameObject.CompareTag("ThinkTrigger"))
        {
            animator.SetBool("isThinking", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("DanceTrigger"))
        {
            animator.SetBool("isDancing", false);
        }
        else if (collision.gameObject.CompareTag("ThinkTrigger"))
        {
            animator.SetBool("isThinking", false);
        }
    }
*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private Vector2 moveInput;
    private Rigidbody rb;

    private void Awake()
    {
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody>();
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
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }
}

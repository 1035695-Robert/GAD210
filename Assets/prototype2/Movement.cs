using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector2 moveInput;
    Rigidbody2D rb;
    InputAction moveAction;
    InputAction leftRotation;
    InputAction RightRotation ;
    private void Awake()
    {
        moveAction = InputManager.Instance.Controls.Player.Move;

        rb = GetComponent<Rigidbody2D>();

    }
    private void OnEnable()
    {
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;

    }


    private void OnDisable()
    {
        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;
        InputManager.Instance.Controls.Disable();
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Debug.Log("pressed");
        moveInput = context.ReadValue<Vector2>();
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void Update()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        Vector2 direction = new Vector2(moveInput.x, moveInput.y).normalized;
        rb.linearVelocity = direction * moveSpeed;

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}


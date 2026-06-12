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


    private float moveValue;
    private Vector2 moveInput;
    private Vector3 rotationDirection;
    Rigidbody2D rb;
    InputAction moving;
   
    InputAction leftRotation;
    InputAction rightRotation;


    bool isMoving, isRotating;
    private void Awake()
    {
        moving = InputManager.Instance.Controls.Player.Move;
        
        leftRotation = InputManager.Instance.Controls.Player.LeftRotate;
        rightRotation = InputManager.Instance.Controls.Player.RightRotate;

        rb = GetComponent<Rigidbody2D>();

        moving.performed += ctx => Update();
    }
    private void OnEnable()
    {
        moving.performed += Moving;
        moving.canceled += StopMoving;
        
        leftRotation.performed += RotateLeft;
        leftRotation.canceled += StopRotation;
       
        rightRotation.performed += RotateRight;
        rightRotation.canceled += StopRotation;
    }



    private void OnDisable()
    {
        moving.performed -= Moving;
        moving.canceled -= StopMoving;
        
        leftRotation.performed -= RotateLeft;
        leftRotation.canceled -= StopRotation;
        rightRotation.performed -= RotateRight;
        rightRotation.canceled -= StopRotation;

        
    }
    private void Update()
    {
        if (!isRotating)
        {
            rb.linearVelocity = transform.up * moveValue;
            if(moveValue == 0)
            {
                Debug.Log("STOP");
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if(!isMoving)
        {
            transform.Rotate(rotationDirection * (rotationSpeed * 10f) * Time.deltaTime);
            rb.linearVelocity = Vector2.zero;
        }

    }
    private void Moving(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<float>();
       
            isMoving= true;

    }
      private void StopMoving(InputAction.CallbackContext context)
    {
        moveValue = 0;
        isMoving= false;

    }
    private void RotateLeft(InputAction.CallbackContext context)
    {
        rotationDirection = Vector3.forward;
        isRotating = true;
    }
    private void RotateRight(InputAction.CallbackContext context)
    {
        rotationDirection = Vector3.back;
        isRotating = true;
    }

    private void StopRotation(InputAction.CallbackContext context)
    {
        isRotating = false;
    }

}


using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
        public Vector2 moveInput;
    CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
      
    }
    private void OnEnable()
    {
        InputManager.Instance.Controls.Player.Up.performed += movement;
       
        
    }
    private void OnDisable()
    {
        InputManager.Instance.Controls.Player.Up.performed -= movement;
        InputManager.Instance.Controls.Disable();




    }

    private void movement(InputAction.CallbackContext context)
    {
        Debug.Log("pressed");
         moveInput = context.ReadValue<Vector2>();
    }
    private void Update()
    {
      characterController.Move(moveInput);
    }
}

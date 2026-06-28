using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;


    private float moveValue;
    private Vector2 moveInput;
    private Vector3 rotationDirection;
    Rigidbody2D rb;
    //movement
    InputAction moving;
    InputAction leftRotation;
    InputAction rightRotation;
    bool isMoving, isRotating;

    //pickUp and Drop
    private Rigidbody2D pickupObject;
    InputAction pickup;
    InputAction dropItem;
    InputAction Throw;
    [SerializeField] private float throwForce;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask PickupLayer;
    [SerializeField] private FixedJoint2D pickupJoint;

    private void Awake()
    {
        moving = InputManager.Instance.Controls.Player.Move;

        leftRotation = InputManager.Instance.Controls.Player.LeftRotate;
        rightRotation = InputManager.Instance.Controls.Player.RightRotate;

        pickup = InputManager.Instance.Controls.Player.PickUp;
        dropItem = InputManager.Instance.Controls.Player.DropItem;
        Throw = InputManager.Instance.Controls.Player.Throw;
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

        pickup.performed += PickUpItem;
        dropItem.performed += DropItem;
        Throw.performed += ThrowItem;
    }



    private void OnDisable()
    {
        moving.performed -= Moving;
        moving.canceled -= StopMoving;

        leftRotation.performed -= RotateLeft;
        leftRotation.canceled -= StopRotation;
        rightRotation.performed -= RotateRight;
        rightRotation.canceled -= StopRotation;

        pickup.performed -= PickUpItem;
        dropItem.performed -= DropItem;

    }
    private void Update()
    {
        if (!isRotating)
        {
            rb.linearVelocity = transform.up * moveValue * moveSpeed;
            if (moveValue == 0)
            {
                Debug.Log("STOP");
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (!isMoving)
        {
            transform.Rotate(rotationDirection * (rotationSpeed * 10f) * Time.deltaTime);
            rb.linearVelocity = Vector2.zero;
        }

    }
    private void Moving(InputAction.CallbackContext context)
    {
       
        moveValue = context.ReadValue<float>();

        isMoving = true;

    }
    private void StopMoving(InputAction.CallbackContext context)
    {
        moveValue = 0;
        isMoving = false;

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

    private void PickUpItem(InputAction.CallbackContext context)
    {
        if (pickupObject != null) return;
        Debug.Log("Try pickup");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayDistance, PickupLayer);
        if (hit.collider != null)
        {
            Debug.Log("Hit");
            pickupJoint = GetComponent<FixedJoint2D>();
            pickupObject = hit.rigidbody;
            //pickupObject.bodyType = RigidbodyType2D.Dynamic;
            pickupJoint.connectedBody = pickupObject;
        }
    }

    private void DropItem(InputAction.CallbackContext context)
    {
        if (pickupObject == null) return;
        Debug.Log("Drop");
        pickupJoint.connectedBody = null;
        //pickupObject.bodyType = RigidbodyType2D.Kinematic;
        pickupObject = null;
    }

    private void ThrowItem(InputAction.CallbackContext context)
    {
        if (pickupObject == null) return;
        Debug.Log("Throw");
        pickupJoint.connectedBody = null;
        pickupObject.AddForce(transform.up * throwForce, ForceMode2D.Impulse);
        pickupObject = null;
    }
}


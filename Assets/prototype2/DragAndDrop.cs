using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{

    [SerializeField] private InputAction mouseClick;
    [SerializeField] private InputAction mouseRelease;
    [SerializeField] LayerMask uiLayer;
    private Camera mainCamera;
    [SerializeField] private float mouseDragPhysicsSpeed = 10;
    [SerializeField] float mouseDragSpeed = .1f;
    private Vector2 velocity = Vector2.zero;
    private WaitForFixedUpdate waitForFixedUpdate;
    private Rigidbody2D rigid;
    private Collider2D col;
    public GameObject clickedObject;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        mouseClick.Enable();
        mouseRelease.Enable();
        mouseClick.performed += MouseClicked;
        mouseRelease.canceled += MouseReleased;
    }
    private void OnDisable()
    {
        mouseClick.Disable();
        mouseRelease.Disable();
        mouseClick.performed -= MouseClicked;
        mouseClick.canceled -= MouseReleased;
    }

    private void MouseReleased(InputAction.CallbackContext context)
    {
        if (clickedObject == null) return;
        Collider2D result = Physics2D.OverlapPoint(clickedObject.transform.position, uiLayer);
        if (result != null && result.gameObject != clickedObject)
        {
            Debug.Log("hello");
            
            KeyBinding bind = result.GetComponent<KeyBinding>();
            bind.SetKey(clickedObject);

        }

        col.isTrigger = false;
        clickedObject = null;
        col = null;
        
    }

    private void MouseClicked(InputAction.CallbackContext context)
    {
        

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && (hit.collider.gameObject.CompareTag("Draggable") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Draggable")))
        {
            clickedObject = hit.collider.gameObject;
            col = clickedObject.GetComponent<Collider2D>();
            col.isTrigger= true;
            StartCoroutine(DragUpdated(clickedObject));
        }
    }
    IEnumerator DragUpdated(GameObject clickedObject)
    {
        clickedObject.TryGetComponent<Rigidbody2D>(out var rb);
        while (mouseClick.ReadValue<float>() != 0)
        {
            float initialDistance = Vector2.Distance(clickedObject.transform.position, mainCamera.transform.position);
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (rb != null)
            {
                Vector2 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.linearVelocity = direction * mouseDragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector2.SmoothDamp(clickedObject.transform.position,
                    ray.GetPoint(initialDistance), ref velocity, mouseDragSpeed);
                yield return null;
            }
        }
        yield return null;
    }
}

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PickUpAndToss : MonoBehaviour
{
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private InputAction mouseRelease;

    public LayerMask targetLayer;
    public LayerMask uiLayer;
    [SerializeField] GameObject holdingObject;
    Collider2D col;
    FixedJoint2D joint2D;

    private void Start()
    {
        joint2D = GetComponent<FixedJoint2D>();
    }

    private void OnEnable()
    {
        mouseClick.Enable();

        mouseClick.performed += Drop;

    }
    private void OnDisable()
    {
        mouseClick.Disable();

        mouseClick.performed -= Drop;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("detected");
        if (holdingObject == null)
        {
            if (collision.transform.CompareTag("Draggable"))
                {
                holdingObject = collision.gameObject;
                col = holdingObject.GetComponent<Collider2D>();
                col.isTrigger = true;
                joint2D.connectedBody = col.GetComponent<Rigidbody2D>();
            }
        }
    }
  
    public void Drop(InputAction.CallbackContext context )
    {
        if (holdingObject == null) return;

        Collider2D result = Physics2D.OverlapPoint(holdingObject.transform.position, uiLayer);
        if (result != null && result.gameObject != holdingObject)
        {
            Debug.Log("hello");

            KeyBinding bind = result.GetComponent<KeyBinding>();
            joint2D.connectedBody = null;
            bind.SetKey(holdingObject);
        }

        col.isTrigger = false;
        holdingObject = null;
        col = null;
    }
}

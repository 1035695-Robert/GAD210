using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace prototype3
{
    public abstract class Input : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D playerRigidbody;
        [SerializeField] private GameObject resetTile;

        [SerializeField] private Vector2 moveValue;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float baseTimer;

        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip clip;
        
        [Header("Input Action References")] [SerializeField]
        protected InputActionReference inputs;

        [SerializeField] protected InputActionReference grabs;


        private void OnEnable()
        {
            PlayerSetUp();
            EventManager.Reset += ResetPlayers;
        }

        protected abstract void PlayerSetUp();

        protected void PlayerInput(InputAction.CallbackContext ctx)
        {
            Debug.Log("Move");
            moveValue = ctx.ReadValue<Vector2>();
        }

        private void Update()
        {
           if(!isHolding) playerRigidbody.linearVelocity = moveValue * moveSpeed;
            else playerRigidbody.linearVelocity = _lockedAxis * moveValue * moveSpeed;

            if (moveValue.sqrMagnitude > 0.01f && !isHolding)
            {
                float targetAngle = (Mathf.Atan2(moveValue.y, moveValue.x) * Mathf.Rad2Deg) - 90;
                Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
                    Time.deltaTime * (rotationSpeed * 10));
            }
            else
            {
                playerRigidbody.angularVelocity = 0f;
            }
        }

        protected void PlayerStopped(InputAction.CallbackContext ctx)
        {
            moveValue = Vector2.zero;
            StartCoroutine(StoppedMovingCountDown());
        }

        private IEnumerator StoppedMovingCountDown()
        {
            float timer = baseTimer;
            source.PlayOneShot(clip);
            while (timer > 0f)
            {
                if (moveValue != Vector2.zero)
                {
                    source.Stop();
                    yield break;
                }
                
                timer -= Time.deltaTime;
                yield return null;
            }

            EventManager.Reset?.Invoke();
            yield return null;
        }

        private void ResetPlayers()
        {
            source.Stop();
            if(_joint != null) Destroy(_joint);
            transform.position = resetTile.transform.position;
            transform.rotation = resetTile.transform.rotation;
            playerRigidbody.linearVelocity = Vector2.zero;
            playerRigidbody.angularVelocity = 0f;
            
        }

        [Header("Pickup Variables")] private Rigidbody2D _dragObject;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask pickupLayer;
        private FixedJoint2D _joint;
        [SerializeField] bool isHolding;

        protected void HoldCheck(InputAction.CallbackContext ctx)
        {
            if (!isHolding) Hold();
            else LetGo();
        }

        private void Hold()
        {
            Debug.Log("Try pickup");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, rayDistance, pickupLayer);
            if (hit.collider != null)
            {
                Debug.Log("Hit");
                AxisLock();
                _joint = transform.AddComponent<FixedJoint2D>();
                _joint.connectedBody = hit.rigidbody;
                _joint.enableCollision = true;
                isHolding = true;
               
            }
        }

        private Vector2 _lockedAxis;

        private void AxisLock()
        {
            float dotX = Mathf.Abs(Vector2.Dot(transform.up, Vector2.right));
            float dotY = Mathf.Abs(Vector2.Dot(transform.up, Vector2.up));

            if(dotX > dotY) _lockedAxis = new Vector2(1f, 0f);
            else _lockedAxis = new Vector2(0f, 1f);
            
        }

        private void LetGo()
        {
            Destroy(_joint);
            AxisUnlock();
            isHolding = false;
            playerRigidbody.linearVelocity = Vector2.zero;
            playerRigidbody.angularVelocity = 0f;
        }

        private void AxisUnlock()
        {
            playerRigidbody.constraints = RigidbodyConstraints2D.None;
        }
    }
}
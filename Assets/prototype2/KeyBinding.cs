using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class KeyBinding : MonoBehaviour
{

    [SerializeField] string BindedKey;
     FixedJoint2D joint;
   

   
    public virtual void Start()
    {
        joint = GetComponent<FixedJoint2D>();
    }
    public void SetKey(GameObject key)
    {
        joint.connectedBody = key.GetComponent<Rigidbody2D>();
        key.transform.position = transform.position;

        BindedKey = key.name;
        string keyPath = $"<keyboard>/{BindedKey.ToLower()}";
        Binding(keyPath);
    }

    public abstract void Binding(string keyPath);
}

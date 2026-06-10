using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyBinding : MonoBehaviour
{

    [SerializeField] string keyName;
     FixedJoint2D joint;
   

   
    public virtual void Start()
    {
        joint = GetComponent<FixedJoint2D>();
    }
    public void SetKey(GameObject key)
    {
        joint.connectedBody = key.GetComponent<Rigidbody2D>();
        key.transform.position = transform.position;

        keyName = key.name;
        string keyPath = $"<keyboard>/{keyName.ToLower()}";
        Binding(keyPath);
    }

    public virtual void Binding(string keyPath)
    {
        
    }
}

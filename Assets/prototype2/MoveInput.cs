using Unity.Burst;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveInput : KeyBinding
{
   public string inputVector;
    public override void Start()
    {
        base.Start();

    }
    public override void Binding(string keyPath)
    {
  
       

        InputManager.Instance.Controls.Player.Up.ApplyBindingOverride(0, keyPath);
        Debug.Log($"{InputManager.Instance.Controls.Player.Up} rebound to {keyPath}");
      
    }
}

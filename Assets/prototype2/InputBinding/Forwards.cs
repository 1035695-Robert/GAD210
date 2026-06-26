using UnityEngine;
using UnityEngine.InputSystem;

public class Forwards : KeyBinding
{
   
    
    public override void Binding(string keyPath)
    {
       
        InputAction ForwardAction = InputManager.Instance.Controls.Player.Move;
        for (int i = 0; i < ForwardAction.bindings.Count; i++)
        {
            if (ForwardAction.bindings[i].isPartOfComposite && ForwardAction.bindings[i].name == "positive")
            {
                ForwardAction.Disable();
                ForwardAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{ForwardAction.bindings[i].name} rebound to {keyPath}");
                ForwardAction.Enable();
                break;
            }
        }
    }
}


using UnityEngine;
using UnityEngine.InputSystem;

public class Backwards : KeyBinding
{
  
    public override void Binding(string keyPath)
    {
        InputAction BackwardsAction = InputManager.Instance.Controls.Player.Move;
        for (int i = 0; i < BackwardsAction.bindings.Count; i++)
        {
            if (BackwardsAction.bindings[i].isPartOfComposite && BackwardsAction.bindings[i].name == "negative")
            {
               BackwardsAction.Disable();
                BackwardsAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{BackwardsAction.bindings[i].name} rebound to {keyPath}");
                BackwardsAction.Enable();
                break;
            }
        }
    }
}

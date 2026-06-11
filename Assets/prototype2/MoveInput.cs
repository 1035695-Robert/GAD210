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
        InputAction moveAction = InputManager.Instance.Controls.Player.Move;

        Debug.Log(moveAction.bindings.ToArray());

        for (int i = 0; i < moveAction.bindings.Count; i++)
        {
            if (moveAction.bindings[i].isPartOfComposite && moveAction.bindings[i].name == inputVector.ToLower())
            {
                InputManager.Instance.Controls.Player.Move.Disable();
                moveAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{moveAction.bindings[i].name} rebound to {keyPath}");
                InputManager.Instance.Controls.Player.Move.Enable();
                break;
            }
        }
    }
}

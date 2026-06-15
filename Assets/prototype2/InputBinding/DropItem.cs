using UnityEngine;
using UnityEngine.InputSystem;

public class DropItem : KeyBinding
{
    public override void Binding(string keyPath)
    {
        InputAction LeftRotateAction = InputManager.Instance.Controls.Player.DropItem;

        LeftRotateAction.Disable();

        LeftRotateAction.ApplyBindingOverride(0, keyPath);

        LeftRotateAction.Enable();

    }
}

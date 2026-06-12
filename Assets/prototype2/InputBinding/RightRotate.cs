using UnityEngine;
using UnityEngine.InputSystem;

public class RightRotate : KeyBinding
{
    public override void Binding(string keyPath)
    {
        InputAction RightRotateAction = InputManager.Instance.Controls.Player.RightRotate;

        RightRotateAction.Disable();

        RightRotateAction.ApplyBindingOverride(0, keyPath);

        RightRotateAction.Enable();

    }
}

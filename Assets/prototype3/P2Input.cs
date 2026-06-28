using UnityEngine.InputSystem;

namespace prototype3
{
    public class P2Input : Input
    {
        protected override void PlayerSetUp()
        {
            
            inputs.action.performed += PlayerInput;
            inputs.action.canceled += PlayerStopped;
            grabs.action.performed += HoldCheck;
            
        }
    }
}
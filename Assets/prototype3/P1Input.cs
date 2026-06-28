namespace prototype3
{
    public class P1Input : Input
    {
        protected override void PlayerSetUp()
        {
            inputs.action.performed += PlayerInput;
            inputs.action.canceled += PlayerStopped;
            grabs.action.performed += HoldCheck;
        }
    }
}

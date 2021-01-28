using UnityEngine;

namespace InputHandler
{
    public class InputMobileHandler : AbstractInputHandler
    {
        public override void HandleInput()
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                LaunchOnTap();
            }
        }
    }
}
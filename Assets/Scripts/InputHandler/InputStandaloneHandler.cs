using UnityEngine;

namespace InputHandler
{
    public class InputStandaloneHandler : AbstractInputHandler
    {
        public override void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                LaunchOnTap();
            }
        }
    }
}
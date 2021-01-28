using UnityEngine;
using UnityEngine.EventSystems;

namespace InputHandler
{
    public class InputMobileHandler : AbstractInputHandler
    {
        public override void HandleInput()
        {
            if(!IsValidTouch())
            {
                return;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                LaunchOnTap();
            }
        }
        
        private bool IsValidTouch()
        {
            return Input.touchCount > 0 & !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }

    }
}
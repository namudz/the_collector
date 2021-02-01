using System;

namespace InputHandler
{
    public abstract class AbstractInputHandler : IInputHandler
    {
        public event Action OnTap;
        
        protected void LaunchOnTap()
        {
            OnTap?.Invoke();
        }

        public abstract void HandleInput();
    }
}
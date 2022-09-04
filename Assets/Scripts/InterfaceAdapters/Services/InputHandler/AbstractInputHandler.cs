using System;

namespace InterfaceAdapters.Services.InputHandler
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
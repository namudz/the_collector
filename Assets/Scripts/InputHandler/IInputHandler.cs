using System;

namespace InputHandler
{
    public interface IInputHandler
    {
        event Action OnTap;
        void HandleInput();
    }
}
using System;

namespace InterfaceAdapters.Services.InputHandler
{
    public interface IInputHandler
    {
        event Action OnTap;
        void HandleInput();
    }
}
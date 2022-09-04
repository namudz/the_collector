using InterfaceAdapters.Services.EventDispatcher;

namespace InterfaceAdapters.Signals
{
    public class ShowLoadingScreenSignal : ISignal
    {
        public bool IsVisible;

        public ShowLoadingScreenSignal(bool isVisible)
        {
            IsVisible = isVisible;
        }
    }
}
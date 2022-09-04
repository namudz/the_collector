namespace Services.EventDispatcher
{
    public interface IEventDispatcher
    {
        void Subscribe<T>(SignalDelegate<T> callback) where T : ISignal;
        void Unsubscribe<T>(SignalDelegate<T> callback) where T : ISignal;
        void Dispatch<T>(T signal) where T : ISignal;
    }
}
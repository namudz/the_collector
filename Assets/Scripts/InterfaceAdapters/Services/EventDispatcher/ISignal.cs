namespace InterfaceAdapters.Services.EventDispatcher
{
    public delegate void SignalDelegate<in T>(T signal) where T : ISignal;
    
    public interface ISignal
    {
        
    }
}
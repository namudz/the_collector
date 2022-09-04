using System;
using System.Collections.Generic;

namespace InterfaceAdapters.Services.EventDispatcher
{
    public class EventDispatcher : IEventDispatcher
    {
        private static class Repository<T> where T : ISignal
        {
            private static readonly IDictionary<IEventDispatcher, IDictionary<Type, SignalDelegate<T>>> _events =
                new Dictionary<IEventDispatcher, IDictionary<Type, SignalDelegate<T>>>();

            public static void Subscribe(IEventDispatcher owner, SignalDelegate<T> signalDelegate)
            {
                _events.TryGetValue(owner, out IDictionary<Type, SignalDelegate<T>> dispatcher);
                if (dispatcher == null)
                {
                    _events.Add(owner, new Dictionary<Type, SignalDelegate<T>>());
                    dispatcher = _events[owner];
                }

                var type = typeof(T);
                if (!dispatcher.ContainsKey(type))
                {
                    dispatcher.Add(type, null);
                }

                dispatcher[type] += signalDelegate;
            }

            public static void Unsubscribe(IEventDispatcher owner, SignalDelegate<T> signalDelegate)
            {
                if(!TryGetEventDispatcher(owner, out var dispatcher)) { return; }

                var type = typeof(T);
                if (dispatcher.ContainsKey(type))
                {
                    dispatcher[type] -= signalDelegate;
                }
            }

            public static void Dispatch(IEventDispatcher owner, T signal)
            {
                if(!TryGetEventDispatcher(owner, out var dispatcher)) { return; }
                
                var type = typeof(T);
                if (dispatcher.ContainsKey(type) && dispatcher[type] != null)
                {
                    dispatcher[type](signal);
                }
            }

            private static bool TryGetEventDispatcher(IEventDispatcher owner, out IDictionary<Type, SignalDelegate<T>> dispatcher)
            {
                _events.TryGetValue(owner, out dispatcher);
                return dispatcher != null;
            }
        }
        
        public void Subscribe<T>(SignalDelegate<T> callback) where T : ISignal
        {
            Repository<T>.Subscribe(this, callback);
        }

        public void Unsubscribe<T>(SignalDelegate<T> callback) where T : ISignal
        {
            Repository<T>.Unsubscribe(this, callback);
        }

        public void Dispatch<T>(T signal) where T : ISignal
        {
            Repository<T>.Dispatch(this, signal);
        }
    }
}
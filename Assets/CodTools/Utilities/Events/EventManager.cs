using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodTools.Utilities
{
    public interface IGameEvent
    {

    }

    public class EventManager : MonoBehaviour
    {
        // singleton instance
        public static EventManager instance;

        private readonly Dictionary<Type, Action<IGameEvent>> _events = 
            new Dictionary<Type, Action<IGameEvent>>();

        private readonly Dictionary<Delegate, Action<IGameEvent>> _eventLookups = 
            new Dictionary<Delegate, Action<IGameEvent>>();

        void Awake() {
            // singleton setup
            if (instance == null) {
                instance = this;
            } else if (instance != this) {
                Destroy(gameObject);
            }
        }

        public void AddListener<T>(Action<T> callback) where T : IGameEvent
        {
            if (_eventLookups.ContainsKey(callback))
            {
                return;
            }

            _eventLookups[callback] = e => callback((T)e);

            var eventType = typeof(T);

            if (_events.TryGetValue(eventType, out Action<IGameEvent> internalAction))
            {
                _events[eventType] = internalAction += _eventLookups[callback];
            }
            else
            {
                _events[eventType] = _eventLookups[callback];
            }
        }

        public void RemoveListener<T>(Action<T> callback) where T : IGameEvent
        {
            if (!_eventLookups.TryGetValue(callback, out Action<IGameEvent> action))
            {
                return;
            }

            var eventType = typeof(T);

            if (_events.TryGetValue(eventType, out Action<IGameEvent> internalAction))
            {
                internalAction -= action;

                if (internalAction is null)
                {
                    _events.Remove(eventType);
                }
                else
                {
                    _events[eventType] = internalAction;
                }
            }

            _eventLookups.Remove(callback);
        }

        public void Raise(IGameEvent evt)
        {
            if (_events.TryGetValue(evt.GetType(), out Action<IGameEvent> action))
            {
                action.Invoke(evt);
            }
        }

        public void Clear()
        {
            _events.Clear();
            _eventLookups.Clear();
        }
    }
}

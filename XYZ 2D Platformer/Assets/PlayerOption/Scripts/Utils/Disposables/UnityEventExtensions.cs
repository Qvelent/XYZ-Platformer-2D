using System;
using UnityEngine.Events;

namespace PlayerOption.Scripts.Utils.Disposables
{
    public static class UnityEventExtensions
    {
        public static IDisposable Subsribe(this UnityEvent unityEvent,UnityAction call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposable(() => unityEvent.RemoveListener(call));
        }
        
        public static IDisposable Subsribe<TType>(this UnityEvent<TType> unityEvent,UnityAction<TType> call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposable(() => unityEvent.RemoveListener(call));
        }
    }
}
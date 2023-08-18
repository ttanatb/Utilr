using System;
using System.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

namespace Utilr.SoGameEvents
{
    public abstract class SoGameEventBase : ScriptableObject
    {
        /// <summary>
        /// Invoke this game event.
        /// </summary>
        [Button("Test Invoke")]
        public abstract void Invoke();

        /// <summary>
        /// Event will auto-invoke on Start, definitely more useful for testing.
        /// </summary>
        [SerializeField]
        private bool m_invokeOnStart = false;

        protected virtual void Awake()
        {
            DelayedInvoke();
        }
        
        /// <summary>
        /// Hack to invoke event after 1 second.
        /// </summary>
        /// <param name="seconds"></param>
        private async void DelayedInvoke(int seconds = 1)
        {
            await Task.Delay(seconds * 1000);
            Invoke();
        }
    }
}

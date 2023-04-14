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
    }
}

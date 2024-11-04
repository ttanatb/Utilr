using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

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
        /// Add listener
        /// </summary>
        /// <param name="action"></param>
        public abstract void AddListener(UnityAction action);

        [Button]
        public abstract void ClearListeners();
    }
}

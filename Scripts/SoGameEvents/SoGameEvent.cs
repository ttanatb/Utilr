using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Utilr.SoGameEvents
{
    [CreateAssetMenu(fileName = "SoGameEvent", menuName = "soVars/SoGameEvent", order = 1)]
    public class SoGameEvent : ScriptableObject
    {
        [field: SerializeField]
        public UnityEvent Event { get; private set; } = new UnityEvent();

        /// <summary>
        /// Invoke this game event.
        /// </summary>
        [Button()]
        public void Invoke()
        {
            Event.Invoke();
        }
    }
}

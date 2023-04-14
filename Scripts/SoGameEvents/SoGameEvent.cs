using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Utilr.SoGameEvents
{
    [CreateAssetMenu(fileName = "SoGameEvent", menuName = "soEvents/SoGameEvent", order = 1)]
    public class SoGameEvent : SoGameEventBase
    {
        [field: SerializeField]
        public UnityEvent Event { get; private set; } = new UnityEvent();

        /// <summary>
        /// Invoke this game event.
        /// </summary>
        [Button()]
        public override void Invoke()
        {
            Event.Invoke();
        }
    }
}

using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Utilr.SoGameEvents
{
    [CreateAssetMenu(fileName = "SoCamFocusEvent", menuName = "soVars/SoCamFocusEvent", order = 1)]
    public class SoCamFocusEvent : ScriptableObject
    {
        [field: SerializeField]
        public UnityEvent<GameObject> Event { get; private set; } = new UnityEvent<GameObject>();

        /// <summary>
        /// Invoke this game event.
        /// </summary>
        [Button()]
        public void Invoke(GameObject focusTarget)
        {
            Event.Invoke(focusTarget);
        }
    }
}

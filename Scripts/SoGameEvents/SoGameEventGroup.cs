using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Utilr.SoGameEvents
{
    /// <summary>
    /// Evokes a group of events
    /// </summary>
    [CreateAssetMenu(fileName = "SoGameEventGroup", menuName = "soEvents/SoGameEventGroup", order = 1)]
    public class SoGameEventGroup : SoGameEventBase
    {
        [field: SerializeField]
        public SoGameEventBase[] GameEvents { get; private set; }

        /// <summary>
        /// Gets the first appearance of event in the list of a certain type
        /// </summary>
        /// <typeparam name="T">Type of event</typeparam>
        /// <returns>Event of specific type</returns>
        public T GetSpecificEvent<T>() where T : class
        {
            return GameEvents.First(element => element is T) as T;
        }

        [Button()]
        public override void Invoke()
        {
            foreach (var evt in GameEvents)
            {
                evt.Invoke();
            }
        }
        public override void AddListener(UnityAction action)
        {
            throw new System.NotImplementedException();
        }
        public override void ClearListeners()
        {
            throw new System.NotImplementedException();
        }


    }
}

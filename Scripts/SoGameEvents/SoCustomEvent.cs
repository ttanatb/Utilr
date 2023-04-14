using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Utilr.SoGameEvents
{
    public class SoCustomEvent<T> : SoGameEventBase
    {
        /// <summary>
        /// Event associated with object
        /// </summary>
        public UnityEvent<T> Event { get; private set; } = new UnityEvent<T>();

        /// <summary>
        /// Data to be passed on using this event.
        /// </summary>
        [field: FormerlySerializedAs("m_testModel")] 
        [field: SerializeField] public T Data { get; set; }
    
        /// <summary>
        /// Invoke the custom event to pass on data.
        /// </summary>
        public virtual void Invoke(T model)
        {
            Data = model;
            Invoke();
        }

        /// <summary>
        /// Invoke with whatever data was already set.
        /// </summary>
        [Button()]
        public override void Invoke()
        {
            Event.Invoke(Data);
        }
    }
}

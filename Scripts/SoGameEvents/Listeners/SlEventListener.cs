using UnityEngine;
using UnityEngine.Events;

namespace Utilr.SoGameEvents
{
    public class SlEventListener : MonoBehaviour
    {
        [SerializeField] protected SoGameEvent m_eventToListenTo = null;
        [SerializeField] protected UnityEvent m_eventToFire = null;

        // Connects the event to listen to with the event to fire next.
        protected UnityAction m_connectiveAction = null;

        protected virtual void OnEnable()
        {
            if (m_connectiveAction == null)
                m_connectiveAction = () => m_eventToFire.Invoke();
            m_eventToListenTo.Event.AddListener(m_connectiveAction);
        }

        protected virtual void OnDestroy()
        {
            m_eventToListenTo.Event.RemoveListener(m_connectiveAction);
        }
    }
}

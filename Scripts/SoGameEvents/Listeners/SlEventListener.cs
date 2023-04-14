using UnityEngine;
using UnityEngine.Events;

namespace Utilr.SoGameEvents
{
    public class SlEventListener : MonoBehaviour
    {
        [SerializeField] protected SoGameEvent m_eventToListenTo = null;
        [SerializeField] private UnityEvent m_eventToFire = null;

        // Saved reference to remove later.
        protected UnityAction m_action = null;

        protected virtual void OnEnable()
        {
            if (m_action == null)
                m_action = () => m_eventToFire.Invoke();
            m_eventToListenTo.Event.AddListener(m_action);
        }

        protected virtual void OnDisable()
        {
            m_eventToListenTo.Event.RemoveListener(m_action);
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

namespace Utilr.SoGameEvents
{
    public class SoListener : MonoBehaviour
    {
        [SerializeField] private SoGameEvent m_eventToListenTo = null;
        [SerializeField] private UnityEvent m_eventToFire = null;

        // Saved reference to remove later.
        private UnityAction m_action = null;

        private void OnEnable()
        {
            m_action = () => m_eventToFire.Invoke();
            m_eventToListenTo.Event.AddListener(m_action);
        }

        private void OnDisable()
        {
            m_eventToListenTo.Event.RemoveListener(m_action);
        }
    }
}

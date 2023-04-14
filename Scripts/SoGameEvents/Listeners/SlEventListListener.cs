using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Utilr.SoGameEvents
{
    public class SlEventListListener : MonoBehaviour
    {
        [SerializeField] protected SoGameEvent[] m_eventsToListenTo = null;
        [SerializeField] private Condition m_condition = Condition.All;
        [SerializeField] private UnityEvent m_eventToFire = null;

        private bool[] m_triggerredTracker = null;
        
        public enum Condition
        {
            All,
            Any,
        }

        // Saved reference to remove later.
        protected readonly UnityAction[] m_actions = null;

        private void Start()
        {
            m_triggerredTracker = new bool[m_eventsToListenTo.Length];
        }

        protected virtual void OnEnable()
        {
            for (int i = 0; i < m_eventsToListenTo.Length; i++)
            {
                var evt = m_eventsToListenTo[i];
                var action = m_actions[i];
                int indexCopy = i;
                action ??= () => FireAction(indexCopy);
                evt.Event.AddListener(action);
            }
        }

        protected virtual void OnDisable()
        {
            for (int i = 0; i < m_eventsToListenTo.Length; i++)
            {
                var evt = m_eventsToListenTo[i];
                var action = m_actions[i];
                evt.Event.RemoveListener(action);
            }
        }

        private void FireAction(int index)
        {
            m_triggerredTracker[index] = true;

            if (!CheckCondition())
                return;

            // Fire event and reset trackers
            m_eventToFire.Invoke();
            for (int i = 0; i < m_triggerredTracker.Length; i++)
                m_triggerredTracker[i] = false;
        }

        private bool CheckCondition()
        {
            return m_condition == Condition.All ? m_triggerredTracker.All(b => b) 
                : m_triggerredTracker.Any(b => b);
        }
    }
}

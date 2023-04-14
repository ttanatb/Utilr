using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions;

namespace Utilr.SoGameEvents
{
    [RequireComponent(typeof(Animator))]
    public class SlTriggerAnimParamListener : MonoBehaviour
    {
        [SerializeField] private SoGameEvent m_gameEvent = null;
        [SerializeField] private Animator m_animator = null;
        [AnimatorParam("m_animator")] [SerializeField]
        private int m_triggerParam = 1;

        private void Start()
        {
            Assert.IsNotNull(m_gameEvent);
            Assert.IsNotNull(m_animator);

            m_gameEvent.Event.AddListener(TriggerAnimParam);
        }

        private void OnDestroy()
        {
            m_gameEvent.Event.RemoveListener(TriggerAnimParam);
        }

        private void TriggerAnimParam()
        {
            m_animator.SetTrigger(m_triggerParam);
        }
    }
}

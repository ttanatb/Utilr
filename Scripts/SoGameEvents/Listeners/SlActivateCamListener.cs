using System;
using UnityEngine;
using Cinemachine;

namespace Utilr.SoGameEvents
{
    [RequireComponent(typeof(CinemachineVirtualCameraBase))]
    public class SlActivateCamListener : SlEventListener
    {
        protected static int m_currentPriority = 10;
        protected CinemachineVirtualCameraBase m_camera = null;

        protected virtual void Start()
        {
            TryGetComponent(out m_camera);
            m_currentPriority = Mathf.Max(m_currentPriority, m_camera.Priority);
        }

        protected override void OnEnable()
        {
            m_connectiveAction = () => {
                m_camera.Priority = ++m_currentPriority;
            };
            base.OnEnable();
        }
    }
}

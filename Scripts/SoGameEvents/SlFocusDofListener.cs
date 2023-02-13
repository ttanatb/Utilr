using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Utilr.SoGameEvents
{
    [RequireComponent(typeof(Volume))]
    public class SlFocusDofListener : MonoBehaviour
    {
        [SerializeField] protected SoCamFocusEvent m_eventToListenTo = null;
        [SerializeField] protected SoCurrentCamera m_camera = null;
        
        private Volume m_volume = null;
        private VolumeProfile m_profile = null;
        private DepthOfField m_dof = null;

        private void Start()
        {
            TryGetComponent(out m_volume);
            m_profile = m_volume.sharedProfile;
            m_dof = (DepthOfField)m_profile.components.First(comp => comp is DepthOfField);
            
            Assert.IsNotNull(m_dof);
        }

        private void FocusOnTarget(GameObject target)
        {
            var dist = transform.position - m_camera.Cam.transform.position;
            m_dof.gaussianStart.value = dist.magnitude;
        }

        private void OnEnable()
        {
            m_eventToListenTo.Event.AddListener(FocusOnTarget);
        }

        private void OnDisable()
        {
            m_eventToListenTo.Event.RemoveListener(FocusOnTarget);
        }
    }
}

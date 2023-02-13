using UnityEngine;

namespace Utilr.SoGameEvents
{
    [RequireComponent(typeof(Camera))]
    public class StSetCurrentCamera : MonoBehaviour
    {
        [SerializeField]
        private SoCurrentCamera m_currentCamera = null;
        
        // Start is called before the first frame update
        private void Start()
        {
            TryGetComponent(out Camera cam);
            m_currentCamera.Cam = cam;
        }
    }
}

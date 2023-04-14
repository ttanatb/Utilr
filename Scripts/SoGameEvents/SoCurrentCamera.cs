using UnityEngine;

namespace Utilr.SoGameEvents
{
    [CreateAssetMenu(fileName = "SoCurrentCamera", menuName = "soEvents/SoCurrentCamera", order = 1)]
    public class SoCurrentCamera : ScriptableObject
    {
        [field: SerializeField]
        public Camera Cam { get; set; } = null;
    }
}

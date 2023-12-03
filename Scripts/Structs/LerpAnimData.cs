using UnityEngine;

namespace Utilr.Structs
{
    /// <summary>
    /// Describes a way to lerp/smooth value over a duration.
    /// </summary>
    /// <typeparam name="T">Type of the param which will be smoothened</typeparam>
    [System.Serializable]
    public struct LerpAnimData<T>
    {
        [field: SerializeField]
        public float Duration { get; set; }
    
        [field: SerializeField]
        public AnimationCurve Curve { get; set; }
    
        [field: SerializeField]
        public T InitialValue { get; set; }
    
        [field: SerializeField]
        public T FinalValue { get; set; }
    }
}

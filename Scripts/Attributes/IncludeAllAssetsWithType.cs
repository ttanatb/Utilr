using System;
using UnityEngine;

namespace Utilr.Attributes {
    
    /// <summary>
    /// This tag can be used on a field of the ScriptableObject type. Whenever a new ScriptableObject with the specific
    /// child type is created (or deleted), it will update the attributed field to match all instances of that type.
    ///
    /// Useful if you want to listen to all instances of a certain type of event.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class IncludeAllAssetsWithType : PropertyAttribute
    {
        public string OnAssignedCb
        { 
            get;
            set;
        }
    }
}

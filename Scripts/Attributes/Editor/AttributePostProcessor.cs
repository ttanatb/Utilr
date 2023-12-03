using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using Utilr.Editor;

namespace Utilr.Attributes.Editor
{
    /// <summary>
    /// Whenever an object with extension .asset is created or deleted,
    /// look through all game objects to fields with the attribute (SubscribeToNewEvents)
    /// then find all scriptable objs for that type and assign it to the field.
    /// </summary>
    public class ScriptableObjPostProcessor : AssetPostprocessor
    {
        private const string SO_EXT = ".asset";
        
        private static void OnPostprocessAllAssets(string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            if (!importedAssets.ContainsTrackedExt(SO_EXT) &&
                !deletedAssets.ContainsTrackedExt(SO_EXT))
                return;

            ProcessAllObjects();
        }

        private static void ProcessAllObjects()
        {
            // Processes all MonoBehaviours
            var allComponents = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>();
            foreach (var component in allComponents)
            {
                // Processes all fields in each MonoBehaviour
                var fields = component.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                foreach (var field in fields)
                {
                    ProcessField(field, component);
                }
            }
        }

        private static void ProcessField(FieldInfo field, MonoBehaviour component)
        {
            if (Attribute.GetCustomAttribute(field, typeof(IncludeAllAssetsWithType))
                is not IncludeAllAssetsWithType attribute) return;

            Assert.IsTrue(field.FieldType.IsArray);
            Assert.IsTrue(field.FieldType.HasElementType);
            var elementType = field.FieldType.GetElementType();
            Assert.IsNotNull(elementType);
            Assert.IsTrue(elementType.IsSubclassOf(typeof(ScriptableObject)));
                    
            // Find all assets of the scriptable object's type.
            object[] foundAssets = Helper.GetAllAssetsOfType(elementType);

            // Sets every field with the SubscribeToNewEvents tag to include all of it.
            var newValue = Array.CreateInstance(elementType, foundAssets.Length);
            Array.Copy(foundAssets, newValue, foundAssets.Length);
            field.SetValue(component, newValue);

            // If set, invoke a function with a given name after assigning.
            if (string.IsNullOrEmpty(attribute.OnAssignedCb)) return;
            component.Invoke(attribute.OnAssignedCb, 0);
        }
    }
}


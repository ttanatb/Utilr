using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using Utilr.Editor;
using Object = System.Object;

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

            ProcessAllSceneObjs();
            ProcessAllAssets();
            Debug.Log("test");
        }

        private static void ProcessAllSceneObjs()
        {
            // Processes all MonoBehaviours
            var allComponents = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>();
            foreach (var component in allComponents)
            {
                // Processes all fields in each MonoBehaviour
                var fields = component.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                foreach (var field in fields)
                {
                    if (ProcessField(field, component))
                    {
                        EditorUtility.SetDirty(component.gameObject);
                    }
                }
            }
        }

        private static void ProcessAllAssets()
        {
            object[] allAssets = Helper.GetAllAssetsOfType(typeof(ScriptableObject));
            foreach (object asset in allAssets)
            {
                if (asset is not ScriptableObject) continue;
                
                // Processes all fields in each MonoBehaviour
                var fields = asset.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                foreach (var field in fields)
                {
                    ProcessField(field, asset);
                }
            }
        }

        private static bool ProcessField(FieldInfo field, object obj)
        {
            if (Attribute.GetCustomAttribute(field, typeof(IncludeAllAssetsWithType))
                is not IncludeAllAssetsWithType attribute) return false;

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

            if (obj is UnityEngine.Object unityObj )
            {
                // Check if prefab
                if (!PrefabUtility.IsPartOfPrefabInstance(unityObj))
                    // Easy reflection if not prefab
                    field.SetValue(unityObj, newValue);
                else
                {
                    // Apply value through serialized object API instead
                    
                    var serializedGameObj = new SerializedObject(unityObj);
                    var prop = serializedGameObj.FindProperty(field.Name);
                    
                    // Resize array to match
                    if (prop.arraySize < newValue.Length)
                    {
                        for (int i = prop.arraySize; i < newValue.Length; i++)
                        {
                            prop.InsertArrayElementAtIndex(i);
                        }
                    }

                    if (prop.arraySize > newValue.Length)
                    {
                        int countToDelete = prop.arraySize - newValue.Length;
                        for (int i = 0; i < countToDelete; i++)
                        {
                            prop.DeleteArrayElementAtIndex(i);
                        }
                    }
                    
                    Assert.AreEqual(prop.arraySize, newValue.Length);

                    for (int i = 0; i < prop.arraySize; i++)
                    {
                        var element = prop.GetArrayElementAtIndex(i);
                        element.objectReferenceValue = newValue.GetValue(i) as UnityEngine.Object;
                    }

                    serializedGameObj.ApplyModifiedProperties();
                }
            }
            else
            {
                
                // Not prefab, can just set value easily
                field.SetValue(obj, newValue);
            }
            

            // If set, invoke a function with a given name after assigning.
            if (string.IsNullOrEmpty(attribute.OnAssignedCb)) return true;
            var method = obj.GetType().GetMethod(attribute.OnAssignedCb);
            Assert.IsNotNull(method);
            method.Invoke(obj, null);

            return true;
        }
    }
}


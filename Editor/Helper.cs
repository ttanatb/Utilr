using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Utilr.Editor
{
    public static partial class Helper
    {
        /// <summary>
        /// Gets all assets that matches the type (used for getting all instances of a scriptable object).
        /// </summary>
        /// <returns></returns>
        public static T[] GetAllAssetsOfType<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            var a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;

        }

        /// <summary>
        /// Gets all assets that matches the type (used for getting all instances of a scriptable object).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object[] GetAllAssetsOfType(Type type)
        {
            string[] guids = AssetDatabase.FindAssets($"t:{type.Name}");
            object[] a = new object[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath(path, type);
            }

            return a;

        }

        public static bool ContainsTrackedExt(this IEnumerable<string> array, string ext)
        {
            return array.Any(s => s.EndsWith(ext));
        }

        public static bool ContainsTrackedExt(this IEnumerable<string> array, IEnumerable<string> extensions)
        {
            return extensions.Any(array.ContainsTrackedExt);
        }
    }
}



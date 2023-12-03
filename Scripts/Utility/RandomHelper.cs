using UnityEngine;

namespace Utilr.Utility
{
    public static partial class Helper
    {
        public static float Range(Vector2 vec)
        {
            return Random.Range(vec.x, vec.y);
        }

        public static T PickFrom<T>(T[] arr)
        {
            return arr[Random.Range(0, arr.Length)];
        }
    }
}

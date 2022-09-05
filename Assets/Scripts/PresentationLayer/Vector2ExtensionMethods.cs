using UnityEngine;

namespace PresentationLayer
{
    public static class Vector2ExtensionMethods
    {
        public static Vector2 Rotate(this Vector2 v, Quaternion q)
        {
            return q * v;
        }
    }
}
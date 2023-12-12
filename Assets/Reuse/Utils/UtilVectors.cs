using System.Collections.Generic;
using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilVectors
    {
        public static Vector2[] TransformListVector3ToArrayVector2(IList<Vector3> list)
        {
            Vector2[] vector2s = new Vector2[list.Count];
            for (int i = 0; i < vector2s.Length; i++)
            {
                vector2s[i] = list[i];
            }

            return vector2s;
        }

        public static Vector3[] TransformListVector2ToArrayVector3(IList<Vector2> list, float z = 0)
        {
            Vector3[] vector3s = new Vector3[list.Count];
            for (int i = 0; i < vector3s.Length; i++)
            {
                vector3s[i] = new Vector3(list[i].x, list[i].y, z);
            }

            return vector3s;
        }
    }
}
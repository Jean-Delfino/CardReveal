using System.Collections.Generic;
using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilMathOperations
    {
        public static List<Vector2> CreateBezierOfTwoPoint(Vector3 referencePoint, Vector2 curveStartPoint, Vector2 targetPoint, Vector2 curveEndPoint, int resolution)
        {
            Vector2 magnitudePoint = referencePoint;
            var points = new List<Vector2>();
            
            for (int j = 0; j < resolution; j++)
            {
                float t = j / (resolution - 1f);
                Vector2 a = Vector2.Lerp(curveStartPoint, targetPoint, t);
                Vector2 b = Vector2.Lerp(targetPoint, curveEndPoint, t);
                Vector2 p = Vector2.Lerp(a, b, t);

                if ((p - magnitudePoint).sqrMagnitude > 0.001f)
                {
                    points.Add(p);
                    magnitudePoint = p;
                }
            }

            return points;
        }
        
        public static float CalculateManhattanDistance(Vector2 a, Vector2 b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
        
        public static float CalculateEuclideanDistance(Vector2 a, Vector2 b)
        {
            return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2));
        }
    }
}
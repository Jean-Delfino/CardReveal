using System.Collections.Generic;
using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilLineRenderer
    {

        public static void CreateLineTrackWithBezier(LineRenderer lineRenderer, Vector2[] anchorPoints, float curveSize, int resolution, bool useWorldSpace = false)
        {
            var drawPoints = new List<Vector2>();
        
            drawPoints.Add(anchorPoints[0]);

            for (int i = 1; i < anchorPoints.Length - 1; i++)
            {
                var targetPoint = anchorPoints[i];
                var targetDir = (anchorPoints[i] - anchorPoints[i - 1]).normalized;
                var dstToTarget = (anchorPoints[i] - anchorPoints[i - 1]).magnitude;
                var dstToCurveStart = Mathf.Max(dstToTarget - curveSize, dstToTarget / 2);
                
                var nextTargetDir = (anchorPoints[i + 1] - anchorPoints[i]).normalized;
                var nextLineLength = (anchorPoints[i + 1] - anchorPoints[i]).magnitude;

                var curveStartPoint = anchorPoints[i - 1] + targetDir * dstToCurveStart;
                var curveEndPoint = targetPoint + nextTargetDir * Mathf.Min(curveSize, nextLineLength / 2);
                
                var res = UtilMathOperations.CreateBezierOfTwoPoint(drawPoints[^1], curveStartPoint, targetPoint, curveEndPoint,
                    resolution);

                foreach (var point in res)
                {
                    drawPoints.Add(point);
                }
            }
            
            drawPoints.Add(anchorPoints[^1]); //Last one

            lineRenderer.positionCount = drawPoints.Count;

            lineRenderer.SetPositions(UtilVectors.TransformListVector2ToArrayVector3(drawPoints));
            lineRenderer.useWorldSpace = useWorldSpace;
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Reuse.CameraControl;

namespace Reuse.Utils
{
    [System.Serializable]
    public class InstanceValues
    {
        #if UNITY_EDITOR
        public string objectName;
        #endif

        public Mesh mesh;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public bool castShadows;
        public Material[] materials;

    }

    public class RenderIDBlock
    {
        public int PositionID;
        public int RotationID;
        public int ScaleID;
    }

    public static class UtilRender
    {
        
        public static void Render(InstanceValues[] instanceValues, MaterialPropertyBlock materialPropertyBlock, RenderIDBlock ids){

            for (int i = 0; i < instanceValues.Length; i++)
            {
                var instance = instanceValues[i];

                materialPropertyBlock.SetVector(ids.PositionID, instance.position);
                materialPropertyBlock.SetVector(ids.RotationID, instance.rotation);
                materialPropertyBlock.SetVector(ids.ScaleID, instance.scale);

                var mesh = instance.mesh;
                
        #if UNITY_EDITOR
                if(mesh.subMeshCount != instance.materials.Length){
                    Debug.Log("WTF " + instance.objectName + " " + mesh.subMeshCount + " " + instance.materials.Length);
                    return;
                }
        #endif

                var matrix = Matrix4x4.TRS(instance.position, Quaternion.Euler(instance.rotation), instance.scale);

                // Draw each submesh with its corresponding material
                for (int j = 0; j < mesh.subMeshCount; j++)
                {
                    var material = instance.materials[j];

                    Graphics.DrawMesh(instance.mesh, 
                                        matrix, 
                                        material, 
                                        0, 
                                        CameraController.GetMainCamera(), 
                                        j, 
                                        materialPropertyBlock, 
                                        instance.castShadows);
                }
            }
        }

        public static IEnumerator OneTimeRender(InstanceValues[] instanceValues, MaterialPropertyBlock materialPropertyBlock, RenderIDBlock ids){
            if(!CameraController.IsMainCameraReady) yield return new WaitUntil(() => CameraController.IsMainCameraReady);
            Render(instanceValues, materialPropertyBlock, ids);

            yield return null;
        }
    }
}


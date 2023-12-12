using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilMaterials
    {
        public static List<MeshRenderer> GetAllMeshRenderersInActor(Transform actor)
        {
            return actor.GetComponentsInChildren<MeshRenderer>().ToList();
        }
        
        public static SkinnedMeshRenderer GetSkinnedMesh(Transform actor)
        {
            return actor.GetComponentInChildren<SkinnedMeshRenderer>();
        }
        
        public static void ChangeSkinnedMeshMaterial(SkinnedMeshRenderer skinnedMesh,Material skinnedMeshMaterial)
        {
            skinnedMesh.material = skinnedMeshMaterial;
        }

        public static void ChangeAllMeshRenderersMaterial(List<MeshRenderer> meshRenderers, Material material)
        {
            if(meshRenderers.Count > 0) meshRenderers.ForEach(mr => mr.material = material);
        }
        
        public static void ChangeAllMeshRenderersMaterialWithNewMaterial(List<MeshRenderer> meshRenderers, Material material)
        {
            if(meshRenderers.Count > 0) meshRenderers.ForEach(mr => mr.material = new Material(material));
        }
        
        public static IEnumerator ChangeDisappearMaterial(SkinnedMeshRenderer skinnedMesh, List<MeshRenderer> meshRenderers, 
            float duration,
            float startValue,
            string name)
        {
            var additionMaterialChangeValue = 0f;
            for (var i = 0f; i < duration && additionMaterialChangeValue + startValue < 1; i += Time.deltaTime)
            {
                additionMaterialChangeValue = (i / duration);
                var dissolveAmount = Mathf.Clamp(startValue + additionMaterialChangeValue, startValue, 1);
                
                ChangeSkinnedAndMeshRenderersMaterialAmount(meshRenderers, skinnedMesh, name, dissolveAmount);

                yield return new WaitForEndOfFrame();
            }
            ChangeSkinnedAndMeshRenderersMaterialAmount(meshRenderers, skinnedMesh, name, 1);
        }
        
        public static IEnumerator ChangeAppearMaterial(SkinnedMeshRenderer skinnedMesh,List<MeshRenderer> meshRenderers, Material material, Material defaultMaterial, float duration, string variableName)
        {
            ChangeSkinnedMeshMaterial(skinnedMesh, new Material(material));
            ChangeAllMeshRenderersMaterialWithNewMaterial(meshRenderers, new Material(material));
            
            float i, dissolveAmount;

            for (i = 0f, dissolveAmount = 1; i < duration; i += Time.deltaTime, dissolveAmount = Mathf.Clamp(1 - (i / duration), 0, 1))
            {
                ChangeSkinnedAndMeshRenderersMaterialAmount(meshRenderers, skinnedMesh, variableName, dissolveAmount);
                yield return new WaitForEndOfFrame();
            }
            
            ChangeSkinnedAndMeshRenderersMaterialAmount(meshRenderers, skinnedMesh, variableName, 0);
            
            yield return null;
            
            ChangeSkinnedMeshMaterial(skinnedMesh, defaultMaterial);
            ChangeAllMeshRenderersMaterialWithNewMaterial(meshRenderers, new Material(defaultMaterial));
        }
        public static (SkinnedMeshRenderer skinnedMesh, List<MeshRenderer> meshRenderers) ChangeMaterial(Transform actor, Material desiredMaterial)
        {
            var skinnedMesh = GetSkinnedMesh(actor);
            if (skinnedMesh == null) return (null, null);

            var meshRenderers = GetAllMeshRenderersInActor(actor);
            ChangeSkinnedMeshMaterial(skinnedMesh,new Material(desiredMaterial));
            ChangeAllMeshRenderersMaterialWithNewMaterial(meshRenderers, desiredMaterial);

            return (skinnedMesh, meshRenderers);
        }
        
        public static void ChangeSkinnedAndMeshRenderersMaterialAmount(List<MeshRenderer> meshRenderers,
            SkinnedMeshRenderer skinnedMesh, string materialName, float value)
        {
            ChangeMaterialVariableAmount(skinnedMesh.material,materialName, value);
            ChangeMeshRenderersMaterialVariableAmount(meshRenderers, materialName, value);
        }
        
        public static void ChangeMaterialVariableAmount(Material material,string materialName, float value)
        {
            material.SetFloat(materialName, value);
        }
        
        public static void ChangeMeshRenderersMaterialVariableAmount(List<MeshRenderer> meshRenderers, string name, float value)
        {
            if (meshRenderers.Count > 0) meshRenderers.ForEach(mr =>  ChangeMaterialVariableAmount(mr.material,name, value));
        }
    }
}
using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilGameObject
    {
        public static void SetActiveStateOfAllTransformChildren(Transform father, bool state)
        {
            foreach (Transform child in father)
            {
                child.gameObject.SetActive(state);
            }
        }
        
        public static GameObject FindParentWithTag(GameObject childObject, string tag)
        {
            Transform t = childObject.transform;
            while (t.parent != null)
            {
                if (t.parent.CompareTag(tag))
                {
                    return t.parent.gameObject;
                }
                t = t.parent.transform;
            }
            return null;
        }
    }
}
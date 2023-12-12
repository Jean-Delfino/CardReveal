using UnityEngine;

namespace Reuse.GameObjectOperations
{
    public class TrailRendererShrinker : MonoBehaviour
    {
        private TrailRenderer trail;
        private const float TrailTimedRemoval = 0.02f;
        private void Start()
        {
            trail = GetComponentInParent<TrailRenderer>();
            trail ??= GetComponent<TrailRenderer>();
        }

        private void Update()
        {
            trail.time = Mathf.Lerp(0, trail.time, TrailTimedRemoval * Time.deltaTime);
        }
    }
}
using UnityEngine;

namespace Reuse.UI
{
    public class SelectionOptionTriggerAnimator : SelectionOption
    {
        [Space][Header("ANIMATOR PARAMETERS")] [Space]
        
        [SerializeField] private Animator animator;
        [SerializeField] private string triggerParameter;
        
        public override void Execute(){
            if(animator && !string.IsNullOrEmpty(triggerParameter)) animator.SetTrigger(triggerParameter);
            base.Execute();
        }
    }
}
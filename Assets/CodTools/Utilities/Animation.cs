using UnityEngine;

namespace CodTools.Utilities
{
    public class Animation
    {
       public static bool IsAnimationFinished(Animator animator, string animationName) {
            AnimatorStateInfo currentAnimState = animator.GetCurrentAnimatorStateInfo(0);
            return currentAnimState.IsName(animationName) 
                && currentAnimState.normalizedTime >= 1;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationTimeControl : MonoBehaviour
{
    public Animator animator;
    [Range(0, 1)] public float animationTime = 0.0f;
    
    void Update()
    {
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, animationTime);
            animator.Update(0);
        }
    }
}

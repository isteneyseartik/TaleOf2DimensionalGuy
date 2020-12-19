using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>(); 
    }
    public void PlayJumpAnim()
    {
        animator.SetBool("IsJumping", true);
    }
    public void PlayIdleAnim()
    {
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsJumping", false);
    }

    public void PlayRunningAnim()
    {
        animator.SetBool("IsRunning", true);
        animator.SetBool("IsJumping", false);
    }
}

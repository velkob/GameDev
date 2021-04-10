using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeciderState : StateMachineBehaviour
{
    [SerializeField]
    private float startRunning;

    [SerializeField]
    private float startJumping;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject player = GameObject.Find("Tim");
        float distanceToPlayer = Vector3.Distance(player.transform.position, animator.transform.position);
        if (distanceToPlayer < startJumping)
        {
            animator.SetBool("ShouldRun", false);
            animator.SetBool("ShouldJump", true);
        }
        else if (distanceToPlayer < startRunning)
        {
            animator.SetBool("ShouldRun", true);
        }
        else
        {
            animator.SetBool("ShouldRun", false);
            animator.SetBool("ShouldJump", false);
        }
    }
}

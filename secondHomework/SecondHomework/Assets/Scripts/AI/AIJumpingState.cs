using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJumpingState : StateMachineBehaviour
{
    [SerializeField]
    private float jumpForce;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<EnemyRunning>().setJumpForce(jumpForce);
    }
}

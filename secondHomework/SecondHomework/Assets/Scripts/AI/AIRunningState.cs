using UnityEngine;

public class AIRunningState : StateMachineBehaviour
{
    [SerializeField]
    private float startJumping;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyRunning>().setSpeed(3);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject player = GameObject.Find("Tim");
        float distanceToPlayer = Vector3.Distance(player.transform.position, animator.transform.position);
        if (distanceToPlayer < startJumping)
        {
            animator.SetBool("ShouldRun", false);
        }
    }
}

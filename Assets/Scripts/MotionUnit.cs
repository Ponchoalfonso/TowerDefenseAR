using UnityEngine;
using UnityEngine.AI;

public class MotionUnit : UnitController
{
    public NavMeshAgent agent;
    public Animator animator;

    protected new void Update()
    {
        base.Update();
        agent.speed = ms;
        if (agent.velocity.magnitude > 0)
        {
            animator.SetBool("Walk", true);
        } else if (agent.velocity.magnitude > 1)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
        } else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
    }

    public new void Kill()
    {
        agent.ResetPath();
        animator.SetTrigger("Die");
    }
}

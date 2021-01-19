using UnityEngine;
using UnityEngine.AI;

public class ChickAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform mother;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(mother.TransformPoint(0, 0, -0.5f));

        if (agent.velocity.magnitude > 0)
        {
            animator.SetBool("Run", true);
        } 
        else
        {
            animator.SetBool("Run", false);
        }
    }
}

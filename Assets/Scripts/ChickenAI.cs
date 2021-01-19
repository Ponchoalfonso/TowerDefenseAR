using UnityEngine;
using UnityEngine.AI;

public class ChickenAI : MonoBehaviour
{

    public float wanderRadius;
    public float wanderTimer;
    public NavMeshAgent agent;
    public Animator animator;

    private float timer;

    void Start()
    {
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            float todo = Random.Range(0, 1.0f);

            if (todo >= 0.6)
            {
                animator.SetTrigger("Eat");
            } else
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
            }
            timer = Random.Range(-4.0f, 4.0f);
        }

        if (agent.velocity.magnitude > 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
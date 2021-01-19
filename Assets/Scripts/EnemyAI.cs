using UnityEngine;

public class EnemyAI : MotionUnit
{
    float timer = 0;

    public UnitController target;
    public GameObject[] destinations;

    public new void Start()
    {
        base.Start();
        GameObject destination = destinations[Random.Range(0, destinations.Length)];
        agent.SetDestination(destination.transform.position);
    }

    protected new void Update()
    {
        if (attacking)
        {
            if (timer <= 0)
            {
                int attack = Random.Range(0, 2);
                animator.SetInteger("RandomAttack", attack);
                animator.SetTrigger("Attack");
                Attack(target);
                timer = 1 / asp;
            } else
            {
                timer -= Time.deltaTime;
            }
        }

        base.Update();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Base")
        {
            agent.ResetPath();
            attacking = true;
        }
    }
}

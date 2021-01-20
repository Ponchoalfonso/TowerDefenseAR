using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnightAI : MotionUnit
{
    GameObject[] enemies;
    EnemyAI target;
    Transform chase;
    float timer = 2;
    float lifeSpan = 15;
    float attack = 0;

    protected new void Start()
    {
        base.Start();
        FetchTarget();
    }

    // Update is called once per frame
    protected new void Update()
    {
        float time = Time.deltaTime;
        if (target)
        {
            if (attacking && agent.velocity.magnitude <= 0)
            {
                if (attack <= 0)
                {
                    int attackIdx = Random.Range(0, 2);
                    animator.SetInteger("RandomAttack", attackIdx);
                    animator.SetTrigger("Attack");
                    Attack(target);
                    if (target.hp <= 0)
                    {
                        target.Kill();
                        target = null;
                        chase = null;
                        agent.isStopped = false;
                        attacking = false;
                    }
                    attack = 1 / asp;
                }
                else
                    attack -= time;
            }
            else
                agent.SetDestination(chase.position);
        } else
        {
            timer -= time;
            if (timer <= 0)
            {
                FetchTarget();
                timer = 2;
            }
        }

        // DogKnights have short lifespan
        lifeSpan -= time;
        if (lifeSpan <= 0)
            Kill();

        base.Update();
    }

    void FetchTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length != 0)
        {
            GameObject obj = enemies[Random.Range(0, enemies.Length)];
            target = obj.GetComponent<EnemyAI>();
            chase = obj.transform;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && other.transform.Equals(chase))
        {
            agent.isStopped = true;
            attacking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && other.transform.Equals(chase))
        {
            agent.isStopped = false;
            attacking = false;
        }
    }
}

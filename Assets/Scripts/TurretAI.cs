using UnityEngine;

public class TurretAI : UnitController
{
    GameObject[] enemies;
    Transform target;
    float timer = 2;
    float firerate;

    float sliderLastX = 0f;

    public Transform turret;
    public Transform shootPoint;
    public ParticleSystem ps;

    new void Start()
    {
        base.Start();
        firerate = 1 / asp;
    }

    new void Update()
    {
        float time = Time.deltaTime;
        if (target)
        {
            Rotate();

            if (firerate <= 0)
            {
                Fire();
                firerate = 1 / asp;
            }

            firerate -= time;
        }
        else
        {
            timer -= time;
            if (timer <= 0)
            {
                FetchTarget();
                timer = 2;
            }
        }
    }

    void FetchTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length != 0)
        {
            target = enemies[Random.Range(0, enemies.Length)].transform;
        }

    }

    void Rotate()
    {
        turret.LookAt(new Vector3(target.position.x, target.position.y, target.position.z));
    }

    void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
                Attack(enemy);
                if (enemy.hp <= 0)
                {
                    enemy.Kill();
                }
            }
        }
        ps.Play();
    }
}

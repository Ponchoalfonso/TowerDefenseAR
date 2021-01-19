using UnityEngine;

public class Projectile : MonoBehaviour
{
    float timer = 3;
    float lifeSpan = 8;
    bool landed = false;

    public float ad;

    private void Update()
    {
        float time = Time.deltaTime;
        if (landed)
        {
            timer -= time;
            if (timer <= 0)
                Object.Destroy(gameObject);
        }

        lifeSpan -= time;
        if (lifeSpan <= 0)
            Object.Destroy(gameObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            var target = other.GetComponent<EnemyAI>();
            target.Damage(ad);
            if (target.hp <= 0)
            {
                target.Kill();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        landed = true;
    }
}

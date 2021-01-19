using UnityEngine;

public class MortarAI : UnitController
{
    GameObject[] enemies;
    Transform target;
    float timer = 2;
    float firerate;

    float sliderLastX = 0f;

    public GameObject projectilePrefab;
    public Transform turret;
    public Transform barrel;
    public Transform launchPoint;
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
            RotateY();
            RotateX();

            if (firerate <= 0)
            {
                Launch();
                firerate = 1 / asp;
            }

            firerate -= time;
        } else
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

    void RotateY()
    {
        turret.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    void RotateX()
    {
        float angle;
        Ballistics.CalculateTrajectory(turret.position, target.TransformPoint(0, 0, 0.5f), 20.0f, out angle);
        Quaternion localRotation = Quaternion.Euler(angle - sliderLastX, 0f, 0f);
        barrel.rotation = barrel.rotation * localRotation;
        sliderLastX = angle;
    }

    void Launch()
    {
        projectilePrefab.GetComponent<Projectile>().ad = ad;
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(launchPoint.forward * 18, ForceMode.Impulse);
        ps.Play();
    }
}

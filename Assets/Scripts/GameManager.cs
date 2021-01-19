using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float timer;
    
    public UnitController mainBase;
    public GameObject[] destinations;
    public GameObject[] spawnPoints;
    public float spawnRate = 10;

    public GameObject spiky;
    public GameObject slime;
    public GameObject beetle;

    public Unit[] beetleVariations;
    public Material[] beetleSkins;

    void Start()
    {
        // Configure missing data in prefabs
        SetEnemyAI(spiky.GetComponent<EnemyAI>());
        SetEnemyAI(slime.GetComponent<EnemyAI>());
        SetEnemyAI(beetle.GetComponent<BeetleAI>());

        timer = spawnRate;
    }

    void Update()
    {
        if (timer <= 0)
        {
            float random = Random.Range(0, 1.0f);
            int totalEnemies;

            if (random < 0.2)
                totalEnemies = 1;
            else if (random >= 0.2 && random < 0.9)
                totalEnemies = 2;
            else
                totalEnemies = 3;

            for (int i = 0; i < totalEnemies; i++)
            {
                SpawnEnemy();
            }

            timer = spawnRate;
        }
        timer -= Time.deltaTime;
    }

    void SetEnemyAI(EnemyAI ai)
    {
        ai.target = mainBase;
        ai.destinations = destinations;
    }

    void RandomizeBeetle()
    {
        BeetleAI ai = beetle.GetComponent<BeetleAI>();
        float random = Random.Range(0, 1.0f);
        int idx;

        if (random < 0.5)
            idx = 0; // Green
        else if (random >= 0.5 && random < 0.8)
            idx = 1; // Purple
        else
            idx = 2; // Red

        ai.unit = beetleVariations[idx];
        ai.skin = beetleSkins[idx];
    }

    void SpawnEnemy()
    {
        float random = Random.Range(0, 1.0f);
        Transform location = spawnPoints[Random.Range(0, 3)].transform;

        if (random < 0.5)
            Instantiate(slime, location);
        else if (random >= 0.5 && random < 0.9)
            Instantiate(spiky, location);
        else
        {
            RandomizeBeetle();
            Instantiate(beetle, location);
        }
    }
}

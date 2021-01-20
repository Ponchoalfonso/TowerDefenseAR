using UnityEngine;
using TMPro;

public enum GameState {
    welcome,
    started,
    ended
}

public class GameManager : MonoBehaviour
{
    public static bool GameIsPaused = false;

    float timer;
    float timeSurvived = 0f;

    GameState gameState = GameState.welcome;

    public UnitController mainBase;
    public GameObject[] destinations;
    public GameObject[] spawnPoints;
    public float spawnRate = 10;

    public GameObject spiky;
    public GameObject slime;
    public GameObject beetle;

    public Unit[] beetleVariations;
    public Material[] beetleSkins;

    // UI
    public HealthBar healthBar;
    public GameObject pauseMenu;
    public TextMeshProUGUI timerDisplay;
    public GameObject startUI;
    public GameObject lostUI;

    void Awake()
    {
        Reset();
    }
    void Start()
    {
        // Configure missing data in prefabs
        SetEnemyAI(spiky.GetComponent<EnemyAI>());
        SetEnemyAI(slime.GetComponent<EnemyAI>());
        SetEnemyAI(beetle.GetComponent<BeetleAI>());

        mainBase.OnDamage += OnDamage;
    }

    void Update()
    {
        float time = Time.deltaTime;
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
        timer -= time;
        timeSurvived += time;
        DisplayTime();
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

    public void DisplayTime()
    {
        int seconds = (int)Mathf.Floor(timeSurvived);
        int minutes = seconds / 60;
        timerDisplay.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    void OnDamage(float health)
    {
        if (health <= 0)
        {
            EndGame();
            return;
        }
        healthBar.SetHealth(health);
    }

    private void OnApplicationPause(bool pause)
    {
        Pause();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            // Pause();
        }
    }

    public void Pause()
    {
        GameIsPaused = true;
        if (gameState == GameState.welcome)
            startUI.SetActive(true);
        else if (gameState == GameState.ended)
            lostUI.SetActive(true);
        else
            pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        GameIsPaused = false;
        pauseMenu.SetActive(false);
        startUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        gameState = GameState.started;
        Resume();
    }

    public void EndGame()
    {
        gameState = GameState.ended;
        Pause();
    }

    public void TogglePause()
    {
        if (GameIsPaused) Resume(); else Pause();
    }

    public void Reset()
    {
        // Remove units
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(unit);
        }
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Defense"))
        {
            Destroy(unit);
        }
        // Reset base
        mainBase.Reset();
        healthBar.SetMaxHealth(mainBase.maxHealth);
        // Reset timers
        timer = spawnRate;
        timeSurvived = 0f;
        // Reset game state and UI
        gameState = GameState.welcome;
        pauseMenu.SetActive(false);
        lostUI.SetActive(false);
        Pause();
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
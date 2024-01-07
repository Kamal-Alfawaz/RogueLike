using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    //public GameObject BossPrefab;
    // The initial difficulty level
    public int difficultyLevel = 1;

    // The maximum difficulty level
    public int maxDifficultyLevel = 5;

    // The time interval between difficulty increases (in seconds)
    public float difficultyInterval = 300f;

    // The timer for the next difficulty increase
    private float difficultyTimer;

    // The enemy prefab to spawn
    public GameObject enemyPrefab;

    // The spawn points for enemies
    public Transform[] spawnPoints;

    // The base spawn rate of enemies (in seconds)
    public float baseSpawnRate = 10f;

    // The spawn rate modifier for each difficulty level
    public float spawnRateModifier = 0.8f;

    // The timer for the next enemy spawn
    public float spawnTimer;

    // The base health of enemies
    public float baseHealth = 100f;

    // The health modifier for each difficulty level
    public float healthModifier = 1.2f;

    // The base damage of enemies
    public float baseDamage = 10f;

    // The damage modifier for each difficulty level
    public float damageModifier = 1.1f;

    // The initial max number of enemies spawned
    public int maxEnemies = 15;

    // The increase in max enemies for each difficulty level
    public int maxEnemiesIncrease = 5;

    // The current number of enemies spawned
    private int currentEnemies = 0;

    void Start()
    {
        // Initialize the timers
        difficultyTimer = difficultyInterval;
        spawnTimer = baseSpawnRate;

        // Initialize the spawnPoints array with the correct size
        spawnPoints = new Transform[11];

        // Assign each spawn point to the spawnPoints array
        for (int i = 0; i < 11; i++)
        {
            string spawnPointName = i == 0 ? "SpawnPoint" : $"SpawnPoint ({i})";
            GameObject spawnPointObject = GameObject.Find(spawnPointName);
            if (spawnPointObject != null)
            {
                spawnPoints[i] = spawnPointObject.transform;
            }
            else
            {
                Debug.LogError($"Spawn point {spawnPointName} not found");
            }
        }
    }

    void Update()
    {
        // Update the timers
        difficultyTimer -= Time.deltaTime;
        spawnTimer -= Time.deltaTime;

        // Check if the difficulty should increase
        if (difficultyTimer <= 0f && difficultyLevel < maxDifficultyLevel)
        {
            // Increase the difficulty level
            difficultyLevel++;

            // Reset the difficulty timer
            difficultyTimer = difficultyInterval;

            // Increase the max number of enemies
            maxEnemies += maxEnemiesIncrease;

            // Display a message to the player
            Debug.Log("Difficulty increased to level " + difficultyLevel);
        }

        // Check if an enemy should spawn and if the current enemies are less than the max enemies
        if (spawnTimer <= 0f && currentEnemies < maxEnemies)
        {
            // Spawn an enemy at a random spawn point
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Adjust the enemy parameters based on the difficulty level
            enemy.GetComponent<Enemy>().maxHealth = baseHealth * Mathf.Pow(healthModifier, difficultyLevel - 1);
            enemy.GetComponent<Enemy>().health = baseHealth * Mathf.Pow(healthModifier, difficultyLevel - 1);
            enemy.GetComponent<Enemy>().damage = baseDamage * Mathf.Pow(damageModifier, difficultyLevel - 1);

            // Increment the current number of enemies
            currentEnemies++;

            // Reset the spawn timer with a modified spawn rate
            spawnTimer = baseSpawnRate * Mathf.Pow(spawnRateModifier, difficultyLevel - 1);
        }
    }

    public void EnemyKilled()
    {
        currentEnemies--;
    }
}

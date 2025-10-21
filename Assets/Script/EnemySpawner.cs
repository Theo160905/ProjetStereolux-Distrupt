using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public float edgePadding = 20f;
    public GameObject enemyPrefab;
    public RectTransform spawnArea;
    public float bpm = 120f;
    public int maxEnemiesToSpawn = 30;

    [Tooltip("Distance minimale entre chaque ennemi en unités UI")]
    public float minDistanceBetweenEnemies = 50f;

    [SerializeField] Contamination contamination;

    private float beatInterval;
    private float timer;
    private int enemiesSpawned = 0;
    private List<Enemy> activeEnemies = new List<Enemy>();

    int enemiesDestroyed = 0;

    public System.Action OnAllEnemiesCleared;

    void Start()
    {
        contamination.SetContamination(maxEnemiesToSpawn, enemyPrefab.GetComponent<Enemy>().lifespan);
        beatInterval = 60f / bpm;
    }

    void Update()
    {
        if (enemiesSpawned >= maxEnemiesToSpawn)
            return;

        timer += Time.deltaTime;
        if (timer >= beatInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos;
        int attempts = 0;
        const int maxAttempts = 10; // pour éviter une boucle infinie

        do
        {
            spawnPos = new Vector2(
                Random.Range(spawnArea.rect.xMin + edgePadding, spawnArea.rect.xMax - edgePadding),
                Random.Range(spawnArea.rect.yMin + edgePadding, spawnArea.rect.yMax - edgePadding)
            );
            attempts++;
        }
        while (!IsPositionValid(spawnPos) && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Impossible de trouver une position valide pour l'ennemi après plusieurs tentatives.");
            return;
        }

        GameObject newEnemy = Instantiate(enemyPrefab, spawnArea);
        newEnemy.GetComponent<RectTransform>().anchoredPosition = spawnPos;

        Enemy enemy = newEnemy.GetComponent<Enemy>();
        enemy.OnDestroyed += HandleEnemyDestroyed;
        activeEnemies.Add(enemy);

        enemiesSpawned++;
    }

    bool IsPositionValid(Vector2 newPosition)
    {
        foreach (Enemy existingEnemy in activeEnemies)
        {
            if (existingEnemy == null) continue;

            RectTransform rt = existingEnemy.GetComponent<RectTransform>();
            if (rt == null) continue;

            if (Vector2.Distance(rt.anchoredPosition, newPosition) < minDistanceBetweenEnemies)
            {
                return false;
            }
        }
        return true;
    }

    void HandleEnemyDestroyed(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
        enemiesDestroyed++;

        if (enemiesDestroyed >= maxEnemiesToSpawn * 0.8f && enemiesSpawned == maxEnemiesToSpawn)
        {
            Debug.Log("Score " + enemiesDestroyed + "/" + maxEnemiesToSpawn);
            OnAllEnemiesCleared?.Invoke();
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public float edgePadding = 20f;
    public GameObject enemyPrefab;
    public RectTransform spawnArea;
    public float bpm = 120f;
    public int maxEnemiesToSpawn = 30;

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
        Vector2 randomPos = new Vector2(
            Random.Range(spawnArea.rect.xMin + edgePadding, spawnArea.rect.xMax - edgePadding),
            Random.Range(spawnArea.rect.yMin + edgePadding, spawnArea.rect.yMax - edgePadding)
        );

        GameObject newEnemy = Instantiate(enemyPrefab, spawnArea);
        newEnemy.GetComponent<RectTransform>().anchoredPosition = randomPos;

        Enemy enemy = newEnemy.GetComponent<Enemy>();
        enemy.OnDestroyed += HandleEnemyDestroyed;
        activeEnemies.Add(enemy);

        enemiesSpawned++;
    }

    void HandleEnemyDestroyed(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
        enemiesDestroyed++;
        Debug.Log("Ennemi détruit. Total détruits: " + enemiesDestroyed);
        if (enemiesDestroyed >= maxEnemiesToSpawn * 0.8f && enemiesSpawned == maxEnemiesToSpawn)
        {
            Debug.Log("50% des ennemis ont été détruits.");
            OnAllEnemiesCleared?.Invoke();
        }
    }

}

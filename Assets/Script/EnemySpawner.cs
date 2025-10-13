
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public float edgePadding = 20f;
    public GameObject enemyPrefab;
    public RectTransform spawnArea;
    public float bpm = 120f;

    private float beatInterval;
    private float timer;
    private List<Enemy> activeEnemies = new List<Enemy>();

    public System.Action OnAllEnemiesCleared;

    void Start()
    {
        beatInterval = 60f / bpm;
    }

    void Update()
    {
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
    Random.Range(spawnArea.rect.yMin + edgePadding, spawnArea.rect.yMax - edgePadding));


        GameObject newEnemy = Instantiate(enemyPrefab, spawnArea);
        newEnemy.GetComponent<RectTransform>().anchoredPosition = randomPos;

        Enemy enemy = newEnemy.GetComponent<Enemy>();
        enemy.OnDestroyed += HandleEnemyDestroyed;
        activeEnemies.Add(enemy);
    }

    void HandleEnemyDestroyed(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
        if (activeEnemies.Count == 0)
        {
            OnAllEnemiesCleared?.Invoke();
        }
    }
}

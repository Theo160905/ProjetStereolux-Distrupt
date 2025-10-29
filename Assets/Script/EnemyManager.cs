using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Liste des ennemis")]
    public List<GameObject> enemyObjects;

    private List<Enemy> enemies = new();
    private int currentEnemyIndex = -1;

    void Start()
    {
        foreach (var obj in enemyObjects)
        {
            if (obj != null && obj.TryGetComponent(out Enemy enemy))
            {
                enemies.Add(enemy);
                enemy.gameObject.SetActive(false);
                enemy.OnEnemyFinished += HandleEnemyFinished;
            }
        }

        Shuffle(enemies);

        ActivateNextEnemy();
    }

    void HandleEnemyFinished(Enemy enemy)
    {
        StartCoroutine(NextEnemyAfterDelay(1f));
    }

    IEnumerator NextEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ActivateNextEnemy();
    }

    void ActivateNextEnemy()
    {
        currentEnemyIndex++;
        if (currentEnemyIndex >= enemies.Count)
        {
            Debug.Log("Tous les ennemis ont été traités !");
            return;
        }

        Enemy nextEnemy = enemies[currentEnemyIndex];
        nextEnemy.gameObject.SetActive(true);
    }

    void Shuffle(List<Enemy> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}

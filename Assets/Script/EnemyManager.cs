using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Liste des ennemis")]
    public List<GameObject> enemyObjects;

    private List<Enemy> enemies = new();
    private int currentEnemyIndex = 0;

    void Start()
    {
        foreach (var obj in enemyObjects)
        {
            if (obj != null && obj.TryGetComponent(out Enemy enemy))
            {
                enemy.enemyManager = this;
                enemies.Add(enemy);
                enemy.gameObject.SetActive(false);
            }
        }

        Shuffle(enemies);
        ActivateNextEnemy();
    }

    public void HandleEnemyFinished(Enemy enemy)
    {
        Debug.Log($"Enemy détruit : {enemy.name}");
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);

        StartCoroutine(NextEnemyAfterDelay(1f));
    }

    public IEnumerator NextEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ActivateNextEnemy();
    }
public void ActivateNextEnemy()
{
    if (enemies == null || enemies.Count == 0)
    {
        Debug.Log("Aucun ennemi dans la liste !");
        return;
    }

    Enemy nextEnemy = enemies[currentEnemyIndex];
    if (nextEnemy.gameObject.activeSelf)
    {
        nextEnemy.InitializeEnemy();
    }
    else
    {
        nextEnemy.gameObject.SetActive(true);
    }
    
    currentEnemyIndex = (currentEnemyIndex + 1) % enemies.Count;
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

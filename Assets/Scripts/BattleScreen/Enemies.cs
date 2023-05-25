using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public List<Enemy> enemies = new();
    private List<Transform> spawnPoints;

    private int countSpawned;

    private void Start()
    {
    }

    private void CheckCountEnemies()
    {
        countSpawned--;
        StartCoroutine(CheckContEnemiesCoroutine());
    }

    private IEnumerator CheckContEnemiesCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        if (countSpawned <= 0)
        {
            GlobalEvents.EndBattle.Invoke();
            GlobalEvents.StressWin.Invoke();
        }
    }

    public void Init(List<EnemySettings> enemiesSettings, List<Transform> spawnPoints)
    {
        this.spawnPoints = spawnPoints;
        countSpawned = CountEnemiesInSettings(enemiesSettings);
        foreach (var enemy in enemiesSettings)
        {
            var newRoutine = StartCoroutine(SpawnEnemies(enemy));
        }
        GlobalEvents.EnemyDie.AddListener(CheckCountEnemies);
    }

    private IEnumerator SpawnEnemies(EnemySettings settings)
    {
        int count = settings.enemyCount;
        while (count > 0)
        {
            count--;
            yield return new WaitForSeconds(settings.coolDownSpawn);
            Enemy enemy = Instantiate(settings.enemyPrefab, RandomPosition(), Quaternion.identity);
            enemy.transform.SetParent(this.transform);
            enemies.Add(enemy);
        }
    }

    private Vector3 RandomPosition()
    {
        Vector3 result = Vector3.zero;
        if (spawnPoints.Count < 0) return result;
        result = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        return result;
    }

    private int CountEnemiesInSettings(List<EnemySettings> enemiesSettings)
    {
        int count = 0;
        foreach (var enemy in enemiesSettings)
        {
            count += enemy.enemyCount;
        }
        return count;
    }
}

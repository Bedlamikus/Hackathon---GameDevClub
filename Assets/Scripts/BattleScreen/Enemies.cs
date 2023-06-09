using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public List<Enemy> enemies = new();
    private List<Transform> spawnPoints;

    private int countSpawned;
    private bool pause;

    private void Start()
    {
        GlobalEvents.Pause.AddListener(Pause);
        GlobalEvents.UnPause.AddListener(UnPause);
        pause = false;
    }

    public void Init(BattlePoint enemiesSettings, List<Transform> spawnPoints)
    {
        this.spawnPoints = spawnPoints;
        countSpawned = CountEnemiesInSettings(enemiesSettings);
        foreach (var enemy in enemiesSettings.enemies)
        {
            var newRoutine = StartCoroutine(SpawnEnemies(enemy));
        }
        GlobalEvents.EnemyDie.AddListener(CheckCountEnemies);
    }

    private IEnumerator SpawnEnemies(EnemiesSettings settings)
    {
        var enemyPrefabs = FindObjectOfType<EnemyPrefabs>();
        int count = settings.count;
        yield return new WaitForSeconds(settings.pauseBeforeSpawn);
        while (count > 0)
        {
            yield return new WaitForSeconds(settings.coolDownBeetwenSpawns);
            if (!pause)
            {
                count--;
                var enemy = Instantiate(enemyPrefabs.enemies[settings.type], RandomPosition(), Quaternion.identity);
                enemy.transform.SetParent(this.transform);
                enemy.Init(settings.health, settings.coolDownAttack, settings.damage, settings.speedMultiplier);
                enemies.Add(enemy);
            }
        }
    }

    private Vector3 RandomPosition()
    {
        Vector3 result = Vector3.zero;
        if (spawnPoints.Count < 0) return result;
        result = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        return result;
    }

    private int CountEnemiesInSettings(BattlePoint enemiesSettings)
    {
        int count = 0;
        foreach (var enemy in enemiesSettings.enemies)
        {
            count += enemy.count;
        }
        return count;
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
            yield return new WaitForSeconds(2.0f);
            GlobalEvents.EndBattle.Invoke();
            GlobalEvents.StressWin.Invoke();
        }
    }

    private void Pause()
    {
        pause = true;
    }
    private void UnPause()
    {
        pause = false;
    }
}

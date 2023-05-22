using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] public List<Enemy> enemies = new List<Enemy>();

    [SerializeField] private List<Transform> spawners = new List<Transform>();
    [SerializeField] private Enemy enemyPrefab;

    [SerializeField] private float coolDown = 2f;

    private int countSpawned;
    private int count;
    private Coroutine spawnEnemies;

    private void Start()
    {
        GlobalEvents.EnemyDie.AddListener(CheckCountEnemies);
    }

    private void CheckCountEnemies()
    {
        StartCoroutine(CheckContEnemiesCoroutine());
    }

    private IEnumerator CheckContEnemiesCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        if (countSpawned <= 0 && enemies.Count <= 0)
        {
            GlobalEvents.EndBattle.Invoke();
            GlobalEvents.StressWin.Invoke();
        }
    }

    public void Init(int enemiesCount)
    {
        count = enemiesCount;
        enemies.Clear();
        if (spawnEnemies != null) StopCoroutine(spawnEnemies);
        spawnEnemies = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        countSpawned = count;
        while (countSpawned > 0)
        {
            countSpawned--;
            yield return new WaitForSeconds(coolDown);
            Enemy enemy = Instantiate(enemyPrefab, RandomPosition(), Quaternion.identity);

            enemies.Add(enemy);
        }
    }

    private Vector3 RandomPosition()
    {
        Vector3 result = Vector3.zero;
        if (spawners.Count < 0) return result;
        result = spawners[Random.Range(0, spawners.Count)].position;
        return result;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] public List<Enemy> enemies = new List<Enemy>();

    [SerializeField] private List<Transform> spawners = new List<Transform>();
    [SerializeField] private Enemy enemyPrefab;

    [SerializeField] private int count = 5;
    [SerializeField] private float coolDown = 2f;

    private int countSpawned;

    private void Start()
    {
        countSpawned = count;
        GlobalEvents.EnemyDie.AddListener(CleanList);
        GlobalEvents.StartBatlle.AddListener(StartSpawned);
    }

    private void StartSpawned()
    {
        StartCoroutine(SpawnEnemies());
     }

    private IEnumerator SpawnEnemies()
    {

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

    private void CleanList(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}

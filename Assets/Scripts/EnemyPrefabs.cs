using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabs : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemiesPrefabs = new List<Enemy>();
    [SerializeField] private List<StressEnemy> stressEnemiesPrefabs = new List<StressEnemy>();
    public Dictionary<string, Enemy> enemies = new Dictionary<string, Enemy>();
    public Dictionary<string, StressEnemy> stressEnemies = new Dictionary<string, StressEnemy>();

    private void Awake()
    {
        foreach (var enemy in enemiesPrefabs)
        {
            enemies.Add(enemy.name, enemy);
        }
        foreach (var stressEnemy in stressEnemiesPrefabs)
        {
            stressEnemies.Add(stressEnemy.name, stressEnemy);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressPoints : MonoBehaviour
{
    [SerializeField] private StressPoint stressPointPrefab;
    [SerializeField] private int countEnemies;
    [SerializeField] private int maxEnemiesInGroup;
    [SerializeField] private StressPositions positions;

    private void Start()
    {
        positions.Init();
        int countGroups = countEnemies / maxEnemiesInGroup;
        int enemiesLeft = countEnemies;
        for (int i = 0; i < countGroups - 1; i++)
        {
            int count = Random.Range(maxEnemiesInGroup/2, maxEnemiesInGroup);
            enemiesLeft -= count;
            CreateStressPoint(count);
        }
        CreateStressPoint(enemiesLeft);
    }

    private void CreateStressPoint(int count)
    {
        var position = positions.Pop();
        var stressPoint = Instantiate(stressPointPrefab, transform);
        stressPoint.transform.position = position.transform.position;
        stressPoint.transform.rotation = position.transform.rotation;
        stressPoint.Init(count);

    }
}

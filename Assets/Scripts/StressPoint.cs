using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressPoint : MonoBehaviour
{
    [SerializeField] private int enemyCount;
    [SerializeField] private StressEnemy enemyPrefab;

    private List<StressEnemy> enemyList = new List<StressEnemy>();

    private void Start()
    {
        int count = (enemyCount - 1) / 10 + 1;
        for (int i = 0; i < count; i++)
        {
            enemyList.Add(Instantiate(enemyPrefab, transform));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Train>()) return;
        GlobalEvents.Pause.Invoke();
        GlobalEvents.StartBattle.Invoke(enemyCount);
    }
}

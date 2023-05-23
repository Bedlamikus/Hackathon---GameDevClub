using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressPoint : MonoBehaviour
{
    [SerializeField] private StressEnemy enemyPrefab;

    private int enemyCount;

    private List<StressEnemy> enemyList = new List<StressEnemy>();

    public void Init(int enemyCount)
    {
        int count = (enemyCount - 1) / 10 + 1;
        for (int i = 0; i < count; i++)
        {
            enemyList.Add(Instantiate(enemyPrefab, transform));
        }
        this.enemyCount = enemyCount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Train>()) return;
        GlobalEvents.TrainStop.Invoke();
        GlobalEvents.StartBattle.Invoke(enemyCount);
        GlobalEvents.StressWin.AddListener(DestroyPoint);
    }
    private void DestroyPoint()
    {
        Destroy(gameObject);
    }
}

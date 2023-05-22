using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressPoint : MonoBehaviour
{
    [SerializeField] private int enemyCount;
    [SerializeField] private Enemy enemyPrefab;

    private List<Enemy> enemyList = new List<Enemy>();

    private void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            enemyList.Add(Instantiate(enemyPrefab, transform));
            enemyList[i].transform.position = new Vector3();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Train>()) return;
        GlobalEvents.Pause.Invoke();
        GlobalEvents.StartBattle.Invoke();
    }
}

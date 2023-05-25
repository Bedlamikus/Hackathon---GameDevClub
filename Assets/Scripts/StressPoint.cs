using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressPoint : MonoBehaviour
{
    [SerializeField] private StressEnemy enemyPrefab;
    [SerializeField] private List<EnemySettings> fightSettings;
    [SerializeField] private MiniGame miniGame;

    private List<StressEnemy> enemyList = new();
    private ScreenFightPosition screenFightPosition;

    public void Start()
    {
        screenFightPosition = FindObjectOfType<ScreenFightPosition>();
        int count = (Count() - 1) / 10 + 1;
        for (int i = 0; i < count; i++)
        {
            enemyList.Add(Instantiate(enemyPrefab, transform));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Train>()) return;
        GlobalEvents.TrainStop.Invoke();
        GlobalEvents.StartBattle.Invoke();
        GlobalEvents.StressWin.AddListener(DestroySelf);
        var game = Instantiate(miniGame, screenFightPosition.transform);
        game.Init(fightSettings);
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private int Count()
    {
        int count = 0;
        foreach (var settings in fightSettings)
        {
            count += settings.enemyCount;
        }
        return count;
    }
}

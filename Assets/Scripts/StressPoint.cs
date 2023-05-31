using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressPoint : MonoBehaviour
{
    [SerializeField] private MiniGame miniGame;

    private BattlePoint battlePoint = null;

    private List<StressEnemy> enemyList = new();
    private ScreenFightPosition screenFightPosition;

    public void Init(BattlePoint battlePoint)
    {
        this.battlePoint = battlePoint;
        screenFightPosition = FindObjectOfType<ScreenFightPosition>();
        var enemyPrefabs = FindObjectOfType<EnemyPrefabs>();
        foreach (var bp in battlePoint.enemies)
        {
            enemyList.Add(Instantiate(enemyPrefabs.stressEnemies[bp.type], transform));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Train>()) return;
        GlobalEvents.TrainStop.Invoke();
        GlobalEvents.StartBattle.Invoke();
        GlobalEvents.StressWin.AddListener(DestroySelf);
        var game = Instantiate(miniGame, screenFightPosition.transform);
        game.Init(battlePoint);
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}

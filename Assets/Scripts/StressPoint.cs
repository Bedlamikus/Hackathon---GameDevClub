using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressPoint : MonoBehaviour
{
    [SerializeField] private MiniGame miniGame;

    private BattlePoint battlePoint = null;

    private List<StressEnemy> enemyList = new();
    private ScreenFightPosition screenFightPosition;
    private List<EnemiesSettings> enemiesSettings;

    public void Init(BattlePoint battlePoint, List<EnemiesSettings> enemiesSettings)
    {
        this.enemiesSettings = enemiesSettings;
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
        var train = other.GetComponent<Train>();
        if (train == null) return;
        train.Pause();
        GlobalEvents.StartBattle.Invoke();
        GlobalEvents.EndBattle.AddListener(DestroySelf);
        var game = Instantiate(miniGame, screenFightPosition.transform);
        game.Init(battlePoint, enemiesSettings);
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [SerializeField] SpeedWay speedWay;
    [SerializeField] BattleTrain train;
    [SerializeField] private List<Transform> spawnPoints = new ();
    [SerializeField] private Enemies enemies;

    private FightSound sound;
    public void Init(BattlePoint points, List<EnemiesSettings> enemiesSettings)
    {
        enemies.Init(points, spawnPoints, enemiesSettings);
        speedWay.Init();
        train.Init();
        GlobalEvents.EndBattle.AddListener(DestroyMiniGame);
        sound = transform.parent.GetComponentInChildren<FightSound>();
        sound.TrainStart();
    }

    public void DestroyMiniGame()
    {
        sound.TrainStop();
        Destroy(gameObject);
    }
}

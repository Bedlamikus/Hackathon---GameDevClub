using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [SerializeField] SpeedWay speedWay;
    [SerializeField] BattleTrain train;
    [SerializeField] private List<Transform> spawnPoints = new ();
    [SerializeField] private Enemies enemies;

    public void Init(BattlePoint settings)
    {
        enemies.Init(settings, spawnPoints);
        speedWay.Init();
        train.Init();
        GlobalEvents.EndBattle.AddListener(DestroyMiniGame);
    }

    public void DestroyMiniGame()
    {
        Destroy(gameObject);
    }
}

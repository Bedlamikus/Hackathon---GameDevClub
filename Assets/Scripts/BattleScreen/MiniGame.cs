using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [SerializeField] SpeedWay speedWay;
    [SerializeField] Enemies enemies;
    [SerializeField] BattleTrain train;

    public void Init(int enemyCount)
    {
        speedWay.Init();
        enemies.Init(enemyCount);
        train.Init();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [SerializeField] SpeedWay speedWay;
    [SerializeField] Enemies enemies;
    [SerializeField] BattleTrain train;
    [SerializeField] int enemiesCount = 10;

    public void Init()
    {
        speedWay.Init();
        enemies.Init(enemiesCount);
        train.Init();
    }

}

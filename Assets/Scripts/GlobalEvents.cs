using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GlobalEvents
{
    public static UnityEvent Pause = new UnityEvent();
    public static UnityEvent UnPause = new UnityEvent();
    public static UnityEvent Restart = new UnityEvent();
    public static UnityEvent StationEnter = new UnityEvent();
    public static UnityEvent StressWin = new UnityEvent();
    public static UnityEvent StressLose = new UnityEvent();
    public static UnityEvent NewLoop = new UnityEvent();

    public static UnityEvent DestroyWay = new UnityEvent();
    public static UnityEvent EndBattle = new UnityEvent();
    public static UnityEvent<int> StartBattle = new UnityEvent<int>();
    public static UnityEvent EnemyDie = new UnityEvent();
    public static UnityEvent BattleTrainDie = new UnityEvent();

    public static UnityEvent<int> ApplyCoins = new UnityEvent<int>();
}

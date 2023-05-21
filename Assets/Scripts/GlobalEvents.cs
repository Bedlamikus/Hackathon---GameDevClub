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
    public static UnityEvent EndBatlle = new UnityEvent();
    public static UnityEvent StartBatlle = new UnityEvent();
    public static UnityEvent<Enemy> EnemyDie = new UnityEvent<Enemy>();
    public static UnityEvent BattleTrainDie = new UnityEvent();

    public static UnityEvent<float> ApplyCoins = new UnityEvent<float>();
}

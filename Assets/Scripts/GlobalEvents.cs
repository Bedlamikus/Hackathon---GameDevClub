using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GlobalEvents
{
    public static UnityEvent TrainStop = new UnityEvent();
    public static UnityEvent Pause = new UnityEvent();
    public static UnityEvent UnPause = new UnityEvent();
    public static UnityEvent TrainGo = new UnityEvent();
    public static UnityEvent<int> Restart = new UnityEvent<int>();
    public static UnityEvent StationEnter = new UnityEvent();
    public static UnityEvent StressWin = new UnityEvent();
    public static UnityEvent StressLose = new UnityEvent();
    public static UnityEvent NewLoop = new UnityEvent();
    public static UnityEvent BuyHealth = new UnityEvent();
    public static UnityEvent DestroyWay = new UnityEvent();
    public static UnityEvent EndBattle = new UnityEvent();
    public static UnityEvent<int> StartBattle = new UnityEvent<int>();
    public static UnityEvent EnemyDie = new UnityEvent();
    public static UnityEvent BattleTrainDie = new UnityEvent();
    public static UnityEvent StartCharlesBattle = new UnityEvent();

    public static UnityEvent<int> ApplyGolds = new UnityEvent<int>();
    public static UnityEvent<float> ApplyDamage = new UnityEvent<float>();
    public static UnityEvent<int> ApplyExperience = new UnityEvent<int>();
    public static UnityEvent<int> ApplyHlam = new UnityEvent<int>();

    public static UnityEvent UpdateUI = new UnityEvent();
}

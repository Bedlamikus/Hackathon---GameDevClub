using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GlobalEvents
{
    public static UnityEvent TrainStop = new();
    public static UnityEvent Pause = new();
    public static UnityEvent UnPause = new();
    public static UnityEvent TrainGo = new();
    public static UnityEvent<int> Restart = new();
    public static UnityEvent StationEnter = new();
    public static UnityEvent StressWin = new();
    public static UnityEvent StressLose = new();
    public static UnityEvent NewLoop = new();
    public static UnityEvent BuyHealth = new();
    public static UnityEvent BuyDamage = new();
    public static UnityEvent DestroyWay = new();
    public static UnityEvent EndBattle = new();
    public static UnityEvent StartBattle = new();
    public static UnityEvent EnemyDie = new();
    public static UnityEvent BattleTrainDie = new();
    public static UnityEvent StartCharlesBattle = new();
    public static UnityEvent SettingsWindow = new();

    public static UnityEvent<int> ApplyGolds = new();
    public static UnityEvent<float> ApplyDamage = new();
    public static UnityEvent<int> ApplyExperience = new();
    public static UnityEvent<int> ApplyHlam = new();
    public static UnityEvent<int> NewExperienseLevel = new();

    public static UnityEvent UpdateUI = new();
    public static UnityEvent<ExcelSettings> DefaultSettingsLoaded = new();
    public static UnityEvent SaveCurrentSettings = new();
    public static UnityEvent LoadCurrentSettings = new();
    public static UnityEvent LoadSettings = new();
    public static UnityEvent<int> ChangeCycleIndex = new();
}

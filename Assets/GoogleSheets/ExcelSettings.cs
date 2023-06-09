using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExcelSettings
{
    public List<CycleSettings> cycleSettings;
    public List<PlayerSettings> playerSettings;
}
[System.Serializable]
public class CycleSettings
{
    public int num;
    public List<BattlePoint> battlePoints;
}

[System.Serializable]
public class BattlePoint
{
    public Vector2Int position;
    public List<EnemiesSettings> enemies;
}
[System.Serializable]
public class EnemiesSettings
{
    public string type;
    public int count;
    public int health;
    public float damage;
    public float coolDownBeetwenSpawns;
    public float pauseBeforeSpawn;
    public float coolDownAttack;
    public float speedMultiplier;
}
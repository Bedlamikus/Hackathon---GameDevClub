using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExcelSettings
{
    public List<CycleSettings> settings;
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
    public float cooldown;
    public int health;
    public float attack;
}
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExcelSettings
{
    public List<CycleSettings> cycleSettings;
    public List<PlayerSettings> playerSettings;
    public List<EnemiesSettings> enemiesSettings;
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
    public List<EnemiesSpawnerByCycle> enemies;
}
[System.Serializable]
public class EnemiesSpawnerByCycle
{
    public string type;
    public int count;
    public float coolDownBeetwenSpawns;
    public float pauseBeforeSpawn;
}
[System.Serializable]
public class EnemiesSettings
{
    public string enemyType;
    public int health;
    public float damage;
    public float speedMultiplier;
    public float coolDownAttack;
    public float scale;
    public int additionHealth;
    public float additionDamage;
    public float additionSpeedMultiplier;
    public float additionCoolDownAttack;
    public float additionScale;
}
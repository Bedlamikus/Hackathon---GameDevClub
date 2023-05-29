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
    public Vector3 position;
    public List<EnemiesSettings> enemies;
}

[System.Serializable]
public class EnemiesSettings
{
    public string type;
    public int count;
    public float cooldown;
}
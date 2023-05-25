using UnityEngine;

[CreateAssetMenu(fileName = "Assets/Scripts/FightScriptableObjects/EnemySettings", menuName = "ScriptableObjects/EnemySettings", order = 1)]
public class EnemySettings : ScriptableObject
{
    public Enemy enemyPrefab;
    public int enemyCount;
    public float coolDownSpawn;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    //Vector2 (health, attack) in every cycles
    [SerializeField] private List<Vector2> cycles = new List<Vector2>();
    private int cycle = 0;

    private void Start()
    {
        GlobalEvents.StationEnter.AddListener(SetNextCycle);
        GlobalEvents.Restart.AddListener(RestartCycle);
    }

    private void RestartCycle(int numCycle)
    {
        cycle = numCycle;
    }

    private void SetNextCycle()
    {
        cycle++;
    }

    public int GetHealth()
    {
        return (int)cycles[cycle].x;
    }

    public float GetAttackDamage()
    {
        return cycles[cycle].y;
    }
}
